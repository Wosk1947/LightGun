
namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    using HuaweiARInternal;
    public class BodySkeletonVisualizer : MonoBehaviour
    {
        private ARBody m_body;
        private static int m_maxSkeletonCnt = 50;
        private static int m_maxSkeletonConnectionCnt = 100;

        private GameObject[] m_skeletonPointObject = new GameObject[m_maxSkeletonCnt];
        private GameObject[] m_lines = new GameObject[m_maxSkeletonConnectionCnt];
        private LineRenderer[] m_skeletonConnectionRenderer = new LineRenderer[m_maxSkeletonConnectionCnt];
        private Material m_skeletonMaterial;

        private Camera m_skeletonCamera;

        private Dictionary<ARBody.SkeletonPointName, ARBody.SkeletonPointEntry> m_bodySkeletons= new Dictionary
            <ARBody.SkeletonPointName, ARBody.SkeletonPointEntry>();
        private List<KeyValuePair<ARBody.SkeletonPointName, ARBody.SkeletonPointName>> m_connections = new List
            <KeyValuePair<ARBody.SkeletonPointName, ARBody.SkeletonPointName>>();
        public void Initialize(ARBody body)
        {
            m_body = body;
            m_skeletonMaterial = new Material(Shader.Find("Diffuse"));

            for (int i = 0; i < m_maxSkeletonCnt; i++)
            {
                m_skeletonPointObject[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_skeletonPointObject[i].transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                m_skeletonPointObject[i].SetActive(false);
            }

            for(int i = 0; i < m_maxSkeletonConnectionCnt; i++)
            {
                m_lines[i] = new GameObject("Lines");
                m_lines[i].transform.localScale = new Vector3(1f, 1f, 1f);
                m_lines[i].transform.position = new Vector3(0f, 0f, 0f);
                m_lines[i].transform.localPosition = new Vector3(0, 0, 0);
                m_lines[i].SetActive(false);

                m_skeletonConnectionRenderer[i] = m_lines[i].AddComponent<LineRenderer>();
                m_skeletonConnectionRenderer[i].positionCount = 2;
                m_skeletonConnectionRenderer[i].startWidth = 0.03f;
                m_skeletonConnectionRenderer[i].endWidth = 0.03f;
            }
            m_skeletonCamera = Camera.main;
            Update();
        }


        public void Update()
        {
            if (null == m_body)
            {
                return;
            }
            ARDebug.LogInfo("body tracking state {0}", m_body.GetTrackingState());
            _DonotShowPointAndConnections();
            if (m_body.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
            }
            else if (m_body.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                _UpdateBody();
            }
        }


        private void _DonotShowPointAndConnections()
        {
            for (int i = 0; i < m_maxSkeletonCnt; i++)
            {
                m_skeletonPointObject[i].SetActive(false);
            }
            for (int i = 0; i < m_maxSkeletonConnectionCnt; i++)
            {
                m_lines[i].SetActive(false);
            }
        }
        private void _UpdateBody()
        {
            m_body.GetSkeletons(m_bodySkeletons);
            foreach (var pair in m_bodySkeletons)
            {
                if (!pair.Value.Is2DValid)
                {
                    continue;
                }
                m_skeletonPointObject[(int)pair.Key].name = pair.Key.ToString();

                Vector3 glCoord = pair.Value.Coordinate2D;
                Vector3 worldCoord = new Vector3((glCoord.x + 1) / 2 ,
                    (glCoord.y + 1) / 2 , 3);

                m_skeletonPointObject[(int)pair.Key].transform.position = m_skeletonCamera.ViewportToWorldPoint(worldCoord);
                m_skeletonPointObject[(int)pair.Key].GetComponent<MeshRenderer>().material = m_skeletonMaterial;
                m_skeletonPointObject[(int)pair.Key].SetActive(true);
            }

            m_body.GetSkeletonConnection(m_connections);
            for (int i = 0; i < m_connections.Count; i++)
            {
                ARBody.SkeletonPointEntry skpStart;
                ARBody.SkeletonPointEntry skpEnd;
                if (!m_bodySkeletons.TryGetValue(m_connections[i].Key, out skpStart) ||
                    !m_bodySkeletons.TryGetValue(m_connections[i].Value, out skpEnd) ||
                    !skpStart.Is2DValid || !skpEnd.Is2DValid)
                {
                    continue;
                }
                Vector3 startGLScreenCoord = skpStart.Coordinate2D;
                Vector3 startScreenCoord = new Vector3((startGLScreenCoord.x + 1) / 2 ,
                    (startGLScreenCoord.y + 1) / 2 , 3);

                Vector3 endGLScreenCoord = skpEnd.Coordinate2D;
                Vector3 endScreenCoord = new Vector3((endGLScreenCoord.x + 1) / 2 ,
                    (endGLScreenCoord.y + 1) / 2 , 3);

                m_skeletonConnectionRenderer[i].SetPosition(0, m_skeletonCamera.ViewportToWorldPoint(startScreenCoord));
                m_skeletonConnectionRenderer[i].SetPosition(1, m_skeletonCamera.ViewportToWorldPoint(endScreenCoord));
                m_skeletonConnectionRenderer[i].gameObject.SetActive(true);
            }
        }


        void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 40;
            if (m_body.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                GUI.Label(new Rect(0, 0, 200, 200), "BodyAction: " + m_body.GetBodyAction(), bb);
            }
        }
    }
}
