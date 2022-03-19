namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    using HuaweiARInternal;
    using System.Diagnostics;

    public class HandVisualizer : MonoBehaviour
    {
        private ARHand m_hand;

        private Camera m_handCamera;
       
        private GameObject m_handBox;
        private LineRenderer m_boxLineRenderer;
        private Material m_skeletonMaterial;
        // 3d objects
        public GameObject m_hwCube;
        public GameObject m_spider;
        public GameObject m_spiderColor;
        public GameObject m_lightBlast;
        public GameObject m_bulbasaur;

        //hand skeleton
        // for now, the Hand AR(Hand skeleton) supports only the Mate 20 Pro and Mate 20 RS front cameras.!!!!!!!!!
        private const int m_kMaxSkeletonPoint = 50;
        private const int m_kMaxSkeletonConnections = 50;
        private GameObject m_handSkeleton;
        private GameObject[] m_handSkeletonPoint = new GameObject[m_kMaxSkeletonPoint];
        private GameObject[] m_handSkeletonLines = new GameObject[m_kMaxSkeletonConnections];
        private LineRenderer[] m_handSkeletonConnectionRenderer = new LineRenderer[m_kMaxSkeletonConnections];


        private Dictionary<ARHand.SkeletonPointName, ARHand.SkeletonPointEntry> m_handSkeletons = new Dictionary
            <ARHand.SkeletonPointName, ARHand.SkeletonPointEntry>();
        private List<KeyValuePair<ARHand.SkeletonPointName, ARHand.SkeletonPointName>> m_connections = new List
            <KeyValuePair<ARHand.SkeletonPointName, ARHand.SkeletonPointName>>();

        public void Initialize(ARHand hand)
        {
            m_hand = hand;
            m_handCamera = Camera.main;

            m_hwCube= GameObject.Find("Cube");
            m_hwCube.SetActive(false);
            m_spider = GameObject.Find("spider3");
            m_spider.SetActive(false);
            m_spiderColor = GameObject.Find("spider");
            m_spiderColor.transform.localScale = new Vector3(0, 0, 0);
            m_bulbasaur = GameObject.Find("bulbasaur");
            m_bulbasaur.SetActive(false);
            m_lightBlast = GameObject.Find("energyBlast");
            m_lightBlast.SetActive(false);

            m_handBox = new GameObject("HandBox");
            m_handBox.transform.localScale = new Vector3(1f, 1f, 1f);
            m_handBox.transform.position = new Vector3(0f, 0f, 0f);
            m_handBox.transform.localPosition = new Vector3(0, 0, 0);
            m_handBox.SetActive(false);

            m_boxLineRenderer = m_handBox.AddComponent<LineRenderer>();
            m_boxLineRenderer.positionCount = 5;
            m_boxLineRenderer.startWidth = 0.03f;
            m_boxLineRenderer.endWidth = 0.03f;


            //hand skeleton
            m_handSkeleton = new GameObject("HandSkeleton");
            m_handSkeleton.SetActive(false);
            for (int i = 0; i < m_kMaxSkeletonPoint; i++)
            {
                m_handSkeletonPoint[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_handSkeletonPoint[i].transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
                m_handSkeletonPoint[i].transform.SetParent(m_handSkeleton.transform, false);
            }
            for (int i = 0; i < m_kMaxSkeletonConnections; i++)
            {
                m_handSkeletonLines[i] = new GameObject();
                m_handSkeletonLines[i].transform.SetParent(m_handSkeleton.transform, false);

                m_handSkeletonConnectionRenderer[i] = m_handSkeletonLines[i].AddComponent<LineRenderer>();
                m_handSkeletonConnectionRenderer[i].positionCount = 2;
                m_handSkeletonConnectionRenderer[i].startWidth = 0.003f;
                m_handSkeletonConnectionRenderer[i].endWidth = 0.003f;
            }

        }
        public void Update()
        {
            if (null == m_hand)
            {
                return;
            }
            _DonotShow3DObjects();
            _DonotShowHandBox();
            if (m_hand.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
            }
            else if (m_hand.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                _UpdateHandBox();
                _UpdateHandSkeleton();
            }
        }

        private void _DonotShowHandBox()
        {
            m_handBox.SetActive(false);
            m_handSkeleton.SetActive(false);
        }
        private void _DonotShow3DObjects()
        {
            m_lightBlast.SetActive(false);
            m_hwCube.SetActive(false);
            m_bulbasaur.SetActive(false);
            m_spider.SetActive(false);
            m_spiderColor.transform.localScale = new Vector3(0, 0, 0);
        }
        private void _UpdateHandBox()
        {
            var handBox = m_hand.GetHandBox();
            //The first one is the left top corner of the rectangle, and the second one is the right bottom corner
            if (handBox.Length < 2)
            {
                ARDebug.LogError("handbox's length is {0}", handBox.Length);
                return;
            }
            Vector3 glLeftTopCorner = handBox[0];
            Vector3 glRightBottomCorner = handBox[1];
            Vector3 glLeftBottomCorner = new Vector3(glLeftTopCorner.x, glRightBottomCorner.y);
            Vector3 glRightTopCorner = new Vector3(glRightBottomCorner.x, glLeftTopCorner.y);

            float glCenterX = (glLeftTopCorner.x + glRightTopCorner.x) / 2;
            float glCenterY = (glLeftTopCorner.y + glLeftBottomCorner.y) / 2;

            Vector3 glCenter = new Vector3(glCenterX, glCenterY);

            m_boxLineRenderer.SetPosition(0, _TransferGLCoord2UnityWoldCoordWithDepth(glLeftTopCorner));
            m_boxLineRenderer.SetPosition(1, _TransferGLCoord2UnityWoldCoordWithDepth(glRightTopCorner));
            m_boxLineRenderer.SetPosition(2, _TransferGLCoord2UnityWoldCoordWithDepth(glRightBottomCorner));
            m_boxLineRenderer.SetPosition(3, _TransferGLCoord2UnityWoldCoordWithDepth(glLeftBottomCorner));
            m_boxLineRenderer.SetPosition(4, _TransferGLCoord2UnityWoldCoordWithDepth(glLeftTopCorner));
            m_boxLineRenderer.material = (Material)Resources.Load("Line", typeof(Material));

            m_boxLineRenderer.startColor = new Color(1, 0, 0, 1);
            m_boxLineRenderer.endColor = new Color(1, 0, 0, 1);
            m_boxLineRenderer.gameObject.SetActive(true);

            m_spider.transform.position = (_TransferGLCoord2UnityWoldCoordWithDepth(glCenter));
            m_lightBlast.transform.position = (_TransferGLCoord2UnityWoldCoordWithDepth(glCenter));
            m_bulbasaur.transform.position = (_TransferGLCoord2UnityWoldCoordWithDepth(glCenter));
            m_spiderColor.transform.position = (_TransferGLCoord2UnityWoldCoordWithDepth(glCenter));
            m_hwCube.transform.position = (_TransferGLCoord2UnityWoldCoordWithDepth(glCenter));
             
            switch (m_hand.GetGestureType())
            {
                case 0:
                    _DonotShow3DObjects();
                    break;
                case 1:
                    _DonotShow3DObjects();
                    m_spider.gameObject.SetActive(true);
                    break;
                case 5:
                    _DonotShow3DObjects();
                    m_lightBlast.gameObject.SetActive(true);
                    break;
                case 6:
                    _DonotShow3DObjects();
                    m_spiderColor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    break;
                case 7:
                    _DonotShow3DObjects();
                    m_hwCube.gameObject.SetActive(true);
                    break;
                case 10:
                    _DonotShow3DObjects();
                    m_bulbasaur.gameObject.SetActive(true);
                    break;
            }
        }

        private void _UpdateHandSkeleton()
        {
          if (m_hand.GetSkeletonCoordinateSystemType() == ARCoordinateSystemType.COORDINATE_SYSTEM_TYPE_3D_CAMERA)
            {
                if (m_skeletonMaterial == null)
                {
                    m_skeletonMaterial = new Material(Shader.Find("Diffuse"));
                }
                Matrix4x4 camera2WorldMatrix = m_handCamera.cameraToWorldMatrix;
                m_handSkeleton.SetActive(true);
                //skeleton point 
                m_hand.GetSkeletons(m_handSkeletons);
                foreach (var skeleton in m_handSkeletons)
                {
                    m_handSkeletonPoint[(int)skeleton.Key].name = skeleton.Key.ToString();
                    Vector3 positionInCameraSpace = skeleton.Value.Coordinate;
                    Vector3 positionInWorldSpace = camera2WorldMatrix.MultiplyPoint(positionInCameraSpace);
                    m_handSkeletonPoint[(int)skeleton.Key].transform.position = positionInWorldSpace;
                    m_handSkeletonPoint[(int)skeleton.Key].GetComponent<MeshRenderer>().material = m_skeletonMaterial;
                }
                //skeleton connection
                m_hand.GetSkeletonConnection(m_connections);
                for (int i = 0; i < m_connections.Count; i++)
                {
                    ARHand.SkeletonPointEntry skpStart;
                    ARHand.SkeletonPointEntry skpEnd;
                    if (!m_handSkeletons.TryGetValue((ARHand.SkeletonPointName)m_connections[i].Key, out skpStart) ||
                        !m_handSkeletons.TryGetValue((ARHand.SkeletonPointName)m_connections[i].Value, out skpEnd))
                    {
                        continue;
                    }
                    Vector3 startCameraCoord = skpStart.Coordinate;
                    Vector3 endCameraCoord = skpEnd.Coordinate;
                    m_handSkeletonConnectionRenderer[i].SetPosition(0, camera2WorldMatrix.MultiplyPoint(startCameraCoord));
                    m_handSkeletonConnectionRenderer[i].SetPosition(1, camera2WorldMatrix.MultiplyPoint(endCameraCoord));
                }
            }


        }

        private Vector3 _TransferGLCoord2UnityWoldCoordWithDepth(Vector3 glCoord)
        {
            Vector3 screenCoord = new Vector3
            {
                x = (glCoord.x + 1) / 2,
                y = (glCoord.y + 1) / 2,
                z = 3,
            };
            return m_handCamera.ViewportToWorldPoint(screenCoord);
        }

        public void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(34f / 255f, 139f / 255f, 34f / 255f);
            bb.fontSize = 42;

            if (m_hand.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                GUI.Label(new Rect(0, 0, 200, 200), string.Format("Gesture Type:{0}",
                    m_hand.GetGestureType()), bb);
            }
        }
    }
}
