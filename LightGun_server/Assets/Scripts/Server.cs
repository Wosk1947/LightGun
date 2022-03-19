using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
    
    [DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    static uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    static uint MOUSEEVENTF_LEFTUP = 0x0004;
    static uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    static uint MOUSEEVENTF_RIGHTUP = 0x0010;
    static uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    static uint MOUSEEVENTF_MIDDLEUP = 0x0040;
    static uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    static uint MOUSEEVENTF_MOVE = 0x0001;
    //[DllImport("user32.dll")]
    // public static extern bool GetCursorPos(out Point pos);

    private  int screenWidth = 1920;
    private  int screenHeight = 1280;

    private  int physicalFactor = 1100;
    private  float customXfactor = 0.769f;

    private  int windowWidth = 640;
    private  int windowHeight = 480;

    private int originX = 0;
    private int originY = 0;

    private float x = 0f;
    private float y = 0f;
    
    private string logMessage = "";
    
    private bool connected = false;
    private string data = "";
    private GameObject spriteObject;

    int port = 9999;

    int maxConnections = 10;
    //private float scale = 0.023f;

    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    // Use this for initialization
    void Start()
    {
        spriteObject = GameObject.Find("crosshair");
        spriteObject.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        // Usually the server doesn't need to draw anything on the screen
        Application.runInBackground = true;
        CreateServer();
    }

    void OnClick(NetworkMessage netMessage)
    {
        var objectMessage = netMessage.ReadMessage<MyNetworkMessage>();
        int click = objectMessage.x;
        int state = objectMessage.y;
        logMessage = "Click: " + click + " State: " + state;
        float factorClickX = 65535f / screenWidth;
        float factorClickY = 65535f / screenHeight;
        float clickX = ((65535 / 2f) + x * factorClickX);
        float clickY = ((65535 / 2f) - y * factorClickY);
        if (click == 0 && state == 0)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)clickX, (uint)clickY, 0, 0);
        }
        if (click == 0 && state == 1)
        {
           mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)clickX, (uint)clickY, 0, 0);
        }
        if (click == 1 && state == 0)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN  | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)clickX, (uint)clickY, 0, 0);
        }
        if (click == 1 && state == 1)
        {
            mouse_event(MOUSEEVENTF_RIGHTUP | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)clickX, (uint)clickY, 0, 0);
        }
        if (click == 2 && state == 0)
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN  | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)clickX, (uint)clickY, 0, 0);
        }
        if (click == 2 && state == 1)
        {
            mouse_event(MOUSEEVENTF_MIDDLEUP | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint) clickX, (uint) clickY, 0,
                0);
        }
    }

    void Update()
    {
        
    }

    void CreateServer()
    {
        // Register handlers for the types of messages we can receive
        RegisterHandlers();

        var config = new ConnectionConfig();
        // There are different types of channels you can use, check the official documentation
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);

        var ht = new HostTopology(config, maxConnections);

        if (!NetworkServer.Configure(ht))
        {
            Debug.Log("No server created, error on the configuration definition");
            return;
        }
        else
        {
            // Start listening on the defined port
            if (NetworkServer.Listen(port))
            {
                Debug.Log("Server created, listening on port: " + port);
                logMessage = "Server created, listening on port: " + port;
            }
            else
                Debug.Log("No server created, could not listen to the port: " + port);
        }
    }

    void OnApplicationQuit()
    {
        NetworkServer.Shutdown();
    }

    private void RegisterHandlers()
    {
        // Unity have different Messages types defined in MsgType
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);

        // Our message use his own message type.
        NetworkServer.RegisterHandler(messageID, OnMessageReceived);
        NetworkServer.RegisterHandler(1034, OnClick);
    }

    private void RegisterHandler(short t, NetworkMessageDelegate handler)
    {
        NetworkServer.RegisterHandler(t, handler);
    }

    void OnClientConnected(NetworkMessage netMessage)
    {
        // Do stuff when a client connects to this server
        connected = true;
    }

    void OnClientDisconnected(NetworkMessage netMessage)
    {
        // Do stuff when a client dissconnects
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<MyNetworkMessage>();
        int dataX = objectMessage.x;
        int dataY = objectMessage.y;
        //Debug.Log(dataX+" "+dataY);
        //logMessage = x.ToString()+" "+y.ToString();
        x = (dataX/10000f) * ((float)screenWidth / (float)windowWidth) * physicalFactor  * customXfactor + originX;
        y = (dataY/10000f) * ((float)screenHeight / (float)windowHeight) * physicalFactor + originY;
        SetCursorPos((int)((Screen.width / 2f)*((float)screenWidth / (float)windowWidth))+(int)x, (int)((Screen.height / 2f)*((float)screenHeight / (float)windowHeight))+(int)(-y));
    }
    private void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;
        bb.normal.textColor = new Color(1, 1, 0);
        bb.fontSize = 20;
        GUI.Label(new Rect(50, 300, 200, 200), logMessage, bb);
        
        screenWidth = int.Parse(GUI.TextField(new Rect(50, 50, 50, 20), screenWidth.ToString(), 25));
        screenHeight = int.Parse(GUI.TextField(new Rect(50, 100, 50, 20), screenHeight.ToString(), 25));
        physicalFactor = int.Parse(GUI.TextField(new Rect(50, 150, 50, 20), physicalFactor.ToString(), 25));
        customXfactor = float.Parse(GUI.TextField(new Rect(50, 200, 50, 20), customXfactor.ToString(), 25));
        //originX = int.Parse(GUI.TextField(new Rect(50, 200, 50, 20), originX.ToString(), 25));
        //originY = int.Parse(GUI.TextField(new Rect(50, 250, 50, 20), originY.ToString(), 25));
    }
}

public class MyNetworkMessage : MessageBase
{
    public int x;
    public int y;
}