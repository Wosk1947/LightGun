using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Xsl;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace Common
{
	using UnityEngine;
	using HuaweiARUnitySDK;
	using System.Collections.Generic;
	using HuaweiARInternal;

	public class HandARController : MonoBehaviour
	{
		private float timer = 0.0f;
		private String saveData = "";
		private Pose currentPose;
		private Pose originPose;
		private Pose maximumHeightPose;
		private bool firstFrame = true;
		private bool calibrate = false;
		private float b;
		private float c;
		private float x0;
		private float y0;
		private float z0;
		private float x;
		private float y;
		private float z;
		private float A = 0;
		private float sinA = 0;
		private float cosA = 1;
		private float xOrigin = 0;
		private float zOrigin = 0;
		private float yOrigin = 0;
		private Vector3 truePos;

		private Vector3 trueRot;

		//Параметры экрана
		private float width = 0.331f;
		private float height = 0.207f;
		private float elevation = 0.025f;
		float baseDeep = 0.250f;
		private float distanceToBase = 0.210f;
		private Vector3 bottomLeft;
		private Vector3 topLeft;
		private Vector3 bottomRight;
		private Vector3 aimVector;
		private Vector3 currentAimVector;
		private Vector2 projectedPoint;


		private string connectionStatus = "Not Connected";
		private bool connected = false;
		private int point = 0;

		int port = 9999;
		private string ipdomain = "192.168.1.";
		private string ipmask = "192.168.1.1";
		string ip = "192.168.1.33";
		short positionMessageID = 1000;
		short clickMessageID = 1034;

		//private string logMessage = "";
		//private string logMessage2 = "";
		//private string logMessage3 = "";

		private bool b0 = false;
		private bool b1 = false;
		private bool b2 = false;
		private int b0counter;
		private int b1counter;
		private int b2counter;

		private int touchX = 0;
		private int touchY = 0;

		NetworkClient client;

		private Vector3 orthoVector;
		private Vector3 originPoint;
		private Vector3 vectorProjectedPoint;
		private Camera camera;
		private Vector3 pointOnScreen;
		private float angle = 0;
		private float distance = 0f;
		private float distanceToScreen = 0f;
		private Vector3 upVector;
		private Vector3 rightVector;
		private Vector3 vectorProbePoint;
		private Vector3 screenProjectionX;
		private Vector3 screenProjectionY;
		private Vector3 vectorFromProbePointToScreenPoint;
		private float screenX = 0f;
		private float screenY = 0f;

		private Vector2 touchPosition;
		private TouchPhase touchPhase;
		private MyNetworkMessage cursorPosition = new MyNetworkMessage();
		private MyNetworkMessage click = new MyNetworkMessage();

		private static Rect r1 = new Rect(50, 50, (int) (Screen.width / 2 - 100), 2 * (int) (Screen.width / 2 - 100));
		private static Rect r2 = new Rect((int) (Screen.width / 2) + 50, 50, (int) (Screen.width / 2 - 100),
			2 * (int) (Screen.width / 2 - 100));
		private static Rect r3 = new Rect((int) (Screen.width / 2) + 50, 50 + 2 * (int) (Screen.width / 2 - 100) + 50,
			(int) (Screen.width / 2 - 100),
			(int) (Screen.width / 2 - 100));
		private Button button1 = new Button(r1,0);
		private Button button2 = new Button(r2,1);
		private Button button3 = new Button(r3,2);
		private string buttonResponse = "";
		List<Button> listOfButtons = new List<Button>();
		void CreateClient()
		{
			var config = new ConnectionConfig();
			config.AddChannel(QosType.ReliableFragmented);
			config.AddChannel(QosType.UnreliableFragmented);
			client = new NetworkClient();
			client.Configure(config, 1);
			RegisterHandlers();
		}

		void Connect()
		{
			client.Connect(ipmask, port);
		}

		void RegisterHandlers()
		{
			client.RegisterHandler(positionMessageID, OnMessageReceived);
			client.RegisterHandler(MsgType.Connect, OnConnected);
			client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
		}

		void OnConnected(NetworkMessage message)
		{
			connected = true;
			connectionStatus = "Connected";
		}

		void OnDisconnected(NetworkMessage message)
		{
		}

		void OnMessageReceived(NetworkMessage netMessage)
		{
		}

		public void Start()
		{
			listOfButtons.Add(button1);
			listOfButtons.Add(button2);
			listOfButtons.Add(button3);

			bottomLeft.Set(0, 0, 0);
			bottomRight.Set(width, 0, 0);
			topLeft.Set(0, height, 0);
			aimVector.Set(0, 0, 0.01f);
			projectedPoint.Set(0, 0);

			camera = Camera.main;
			originPoint.Set(0, 0, 0);
			orthoVector.Set(0, 0, 1);
			pointOnScreen.Set(0, 0, 0);
			upVector.Set(0, 1, 0);
			rightVector.Set(1, 0, 0);
			CreateClient();
		}

		public void Update()
		{
			if (connected)
			{

				for (int i = 0; i < Input.touchCount; i++)
				{
					for (int b=0; b<listOfButtons.Count; b++)
					{
						String response = listOfButtons[b].processTouch(Input.GetTouch(i));
						//logMessage+="\nButton: "+listOfButtons[b].id+" Touch: "+i+" Result: "+response;
						if (response == "justPressed")
						{
							//logMessage3 = "Press";
							sendClick(listOfButtons[b].id,0);
						}
						if (response == "justReleased")
						{
							//logMessage3 = "Release";
							sendClick(listOfButtons[b].id,1);
						}
					}
				}
			}

			if (calibrate)
			{
				orthoVector = camera.transform.up;
				originPoint = ARFrame.GetPose().position;
				orthoVector.Normalize();
				pointOnScreen = originPoint + orthoVector * 0.022f;
				upVector = camera.transform.right.normalized * (+1f);
				rightVector = camera.transform.forward.normalized * (+1f);
				calibrate = false;
			}

			distanceToScreen = Vector3.Distance(ARFrame.GetPose().position, pointOnScreen);
			vectorProbePoint = Vector3.zero;
			for (int i = 0; i < 1000; i++)
			{
				vectorProbePoint = ARFrame.GetPose().position + camera.transform.right.normalized * (+0.015f) +
				                   camera.transform.forward.normalized * (-0.003f) +
				                   i * (0.01f) * camera.transform.up.normalized;
				vectorFromProbePointToScreenPoint = vectorProbePoint - pointOnScreen;
				angle = Vector3.Angle(orthoVector, vectorFromProbePointToScreenPoint);
				if (Math.Abs(angle) <= 90)
				{
					vectorProjectedPoint = vectorProbePoint;
					distance = Vector3.Distance(vectorProjectedPoint, pointOnScreen);
					screenProjectionY = Vector3.Project(vectorFromProbePointToScreenPoint, upVector);
					screenProjectionX = Vector3.Project(vectorFromProbePointToScreenPoint, rightVector);
					if (Math.Abs(Vector3.Angle(upVector, screenProjectionY)) > 90)
					{
						screenY = -1 * screenProjectionY.magnitude;
					}
					else
					{
						screenY = screenProjectionY.magnitude;
					}

					if (Math.Abs(Vector3.Angle(rightVector, screenProjectionX)) > 90)
					{
						screenX = -1 * screenProjectionX.magnitude;
					}
					else
					{
						screenX = screenProjectionX.magnitude;
					}

					if (connected)
					{
						cursorPosition.x = (int) (screenX * 10000);
						cursorPosition.y = (int) (screenY * 10000);
						client.Send(positionMessageID, cursorPosition);
						//logMessage2 = "pos sent";
					}

					break;
				}
			}
		}
		void sendClick(int button, int state)
		{
			click.x = button;
			click.y = state;
			client.Send(clickMessageID, click);
		}

		private void OnGUI()
		{
			GUIStyle bb = new GUIStyle();
			bb.normal.background = null;
			bb.normal.textColor = new Color(1, 1, 0);
			bb.fontSize = 50;
			int width = 50;

			if (!connected)
			{
				//GUI.Label(new Rect(100, 200, width, 200), "d. to screen: " + distanceToScreen.ToString(), bb);
				//GUI.Label(new Rect(100, 300, width, 200), "angle of impact " + angle.ToString(), bb);
				//GUI.Label(new Rect(100, 400, width, 200), "d. from origin to imp " + distance.ToString(), bb);
				//GUI.Label(new Rect(100, 500, width, 200), "up " + upVector.ToString("0.000"), bb);
				//GUI.Label(new Rect(100, 600, width, 200), "right " + rightVector.ToString("0.000"), bb);
				//GUI.Label(new Rect(100, 700, width, 200),
				//	"vec. from 0 to imp " + vectorFromProbePointToScreenPoint.ToString("00.000"), bb);
				//GUI.Label(new Rect(100, 800, width, 200), "projY " + screenProjectionY.ToString("00.000"), bb);
				//GUI.Label(new Rect(100, 900, width, 200), "projX " + screenProjectionX.ToString("00.000"), bb);
				//GUI.Label(new Rect(100, 100, width, 200),
				//	"point " + screenX.ToString("0.000") + ":" + screenY.ToString("0.000"), bb);
				if (
					GUI.Button(new Rect(50, Screen.height - 350, 300, 300), "CALIBRATE"))
				{
					calibrate = true;
				}


				if (
					GUI.Button(new Rect(50, Screen.height - 700, 300, 300), "CONNECT"))
				{
					Connect();
				}

				ipmask = GUI.TextField(new Rect(50, 50, 300, 100), ipmask, 25);
			}

			if (connected)
			{
				if (GUI.Button(r1, "0: "+button1.state))
				{
				}
				if (GUI.Button(r2, "1: "+button2.state))
				{
				}
				if (GUI.Button(r3, "2: "+button3.state))
				{
				}
				//GUI.Label(new Rect(100, 50, width, 200), button2.log.Substring(button2.log.Length-60 < 0? 0: button2.log.Length-60), bb);
				//GUI.Label(new Rect(100, 250, width, 200), button3.log.Substring(button3.log.Length-60 < 0? 0: button3.log.Length-60), bb);
				//GUI.Label(new Rect(100, 450, width, 200), logMessage.Substring(logMessage.Length-350 < 0? 0: logMessage.Length - 350), bb);
				//bb.fontSize = 50;
				//GUI.Label(new Rect(100, 600, width, 200), logMessage3, bb);
				//bb.fontSize = 20;
			}
		}
	}
}

public class MyNetworkMessage : MessageBase
{
	public int x;
	public int y;
}

public class Button
{
	public int id;
	public String state = "free";
	public String log = "";


	private int touchX = 0;
	private int touchY = 0;

	private int x;
	private int y;
	private int width;
	private int height;

	public Button(Rect rect, int id)
	{
		this.x = (int) rect.x;
		this.y = (int) rect.y;
		this.width = (int) rect.width;
		this.height = (int) rect.height;
		this.id = id;
	}

	private bool isOverButton(Vector2 touch)
	{
		int touchX = (int)touch.x;
		int touchY = (int)(Screen.height - touch.y);
		return (touchX >= x && touchX <= x + width && touchY >= y && touchY <= y + height);
	}

	public String processTouch(Touch touch)
	{
		String response = "";
		if (isOverButton(touch.position))
		{
			if ((touch.phase == TouchPhase.Began) &&( state.Equals("free")))
			{
				state = "pressed";
				log+=("\njustPressed");
				response = "justPressed";
			}

			if ((touch.phase == TouchPhase.Ended) && (state.Equals("pressed")))
			{
				state = "free";
				log+=("\njustReleased");
				response = "justReleased";
			}
		}
		return response;
	}
}