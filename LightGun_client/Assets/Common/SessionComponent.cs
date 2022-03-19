﻿namespace Common
{
    using HuaweiARUnitySDK;
    using UnityEngine;
    using HuaweiARInternal;

    public class SessionComponent : MonoBehaviour
    {
        [Tooltip("config")]
        public ARConfigBase Config;


        private bool isFirstConnect = true;//this is used to avoid multiple permission request when it was rejected
        private bool isSessionCreated =false;
        private bool isErrorHappendWhenInit = false;

        private string errorMessage = null;
        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void Start()
        {
            //Init();
        }

        bool installRequested = false;
        void Init()
        {
            //If you do not want to switch engines, AREnginesSelector is useless.
            // You just need to use AREnginesApk.Instance.requestInstall() and the default engine
            // is Huawei AR Engine.
            AREnginesAvaliblity ability = AREnginesSelector.Instance.CheckDeviceExecuteAbility();
            if ((AREnginesAvaliblity.HUAWEI_AR_ENGINE & ability) != 0)
            {
                AREnginesSelector.Instance.SetAREngine(AREnginesType.HUAWEI_AR_ENGINE);
            }
            /*else if((AREnginesAvaliblity.GOOGLE_AR_CORE&ability) != 0)
            {
                AREnginesSelector.Instance.SetAREngine(AREnginesType.GOOGLE_AR_CORE);
            }*/
            else
            {
                errorMessage ="This device does not support AR Engine. Exit.";
                Invoke("_DoQuit", 0.5f);
                return;
            }

            try
            {
                switch (AREnginesApk.Instance.RequestInstall(!installRequested))
                {
                    case ARInstallStatus.INSTALL_REQUESTED:
                        installRequested = true;
                        return;
                    case ARInstallStatus.INSTALLED:
                        break;
                }

            }
            catch (ARUnavailableConnectServerTimeOutException e)
            {
                errorMessage ="Network is not available, retry later!";
                Invoke("_DoQuit", 0.5f);
                return;
            }
            catch (ARUnavailableDeviceNotCompatibleException e)
            {
                errorMessage ="This Device does not support AR!";
                Invoke("_DoQuit", 0.5f);
                return;
            }
            catch (ARUnavailableEmuiNotCompatibleException e)
            {
                errorMessage ="This EMUI does not support AR!";
                Invoke("_DoQuit", 0.5f);
                return;
            }
            catch (ARUnavailableUserDeclinedInstallationException e)
            {
                errorMessage ="User decline installation right now, quit";
                Invoke("_DoQuit", 0.5f);
                return;
            }
            if (isFirstConnect)
            {
                _Connect();
                isFirstConnect = false;
            }
        }
        public void Update()
        {
            _AppQuitOnEscape();
            AsyncTask.Update();
            ARSession.Update();
        }


        public void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                ARSession.Pause();
            }
            else
            {
                if (!isSessionCreated)
                {
                    Init();
                }
                if (isErrorHappendWhenInit)
                {
                    return;
                }
                try
                {
                    ARSession.Resume();
                }
                catch (ARCameraPermissionDeniedException e)
                {
                    ARDebug.LogError("camera permission is denied");
                    errorMessage="This app require camera permission";
                    Invoke("_DoQuit", 0.5f);
                }
            }
        }

        public void OnApplicationQuit()
        {
            ARSession.Stop();
            isFirstConnect = true;
            isSessionCreated = false;
        }

        private void _Connect()
        {
            ARDebug.LogInfo("_connect begin");
            const string ANDROID_CAMERA_PERMISSION_NAME = "android.permission.CAMERA";
            if (AndroidPermissionsRequest.IsPermissionGranted(ANDROID_CAMERA_PERMISSION_NAME))
            {
                _ConnectToService();
                return;
            }
            var permissionsArray = new string[] { ANDROID_CAMERA_PERMISSION_NAME };
            AndroidPermissionsRequest.RequestPermission(permissionsArray).ThenAction((requestResult) =>
            {
                if (requestResult.IsAllGranted)
                {
                    _ConnectToService();
                }
                else
                {
                    ARDebug.LogError("connection failed because a needed permission was rejected.");
                    errorMessage="This app require camera permission";
                    Invoke("_DoQuit", 0.5f);
                    return;
                }
            });
        }
        private void _ConnectToService()
        {
            try
            {
                ARSession.CreateSession();
                isSessionCreated = true;
                ARSession.Config(Config);
                ARSession.Resume();
                ARSession.SetCameraTextureNameAuto();
                ARSession.SetDisplayGeometry(Screen.width, Screen.height);
            }
            catch (ARCameraPermissionDeniedException e)
            {
                isErrorHappendWhenInit = true;
                ARDebug.LogError("camera permission is denied");
                errorMessage="This app require camera permission";
                Invoke("_DoQuit", 0.5f);
            }
            catch (ARUnavailableDeviceNotCompatibleException e)
            {
                isErrorHappendWhenInit = true;
                errorMessage="This device does not support AR";
                Invoke("_DoQuit", 0.5f);
            }
            catch (ARUnavailableServiceApkTooOldException e)
            {
                isErrorHappendWhenInit = true;
                errorMessage="This AR Engine is too old, please update";
                Invoke("_DoQuit", 0.5f);
            }
            catch (ARUnavailableServiceNotInstalledException e)
            {
                isErrorHappendWhenInit = true;
                errorMessage="This app depend on AREngine.apk, please install it";
                Invoke("_DoQuit", 0.5f);
            }
            catch(ARUnSupportedConfigurationException e)
            {
                isErrorHappendWhenInit = true;
                errorMessage="This config is not supported on this device, exit now.";
                Invoke("_DoQuit", 0.5f);
            }
        }

        private void _AppQuitOnEscape()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Invoke("_DoQuit", 0.5f);
            }
        }

        private void _DoQuit()
        {
            Application.Quit();
        }

        private void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 45;

            GUI.Label(new Rect(0, Screen.height-100, 200, 200), errorMessage, bb);
        }
    }
}
