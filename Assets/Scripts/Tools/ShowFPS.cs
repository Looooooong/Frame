using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour {


    public enum ServerType
    {
        DevelopServer = 0,//开发服务器
        DemoServer = 1//演示服务器
    }



    public ServerType serverType;
    public bool isShowFrameUpdate = true;//是否显示帧数
    private float m_LastUpdateShowTime = 0f;    //上一次更新帧率的时间;
    private float m_UpdateShowDeltaTime = 0.5f;//更新帧率的时间间隔;
    private int m_FrameUpdate = 0;//帧数;
    private float m_FPS = 0;

    Ping ping;
    string serverAddress = "127.0.0.1";
    float delayTime;


    private void Awake()
    {

        if (serverType == ServerType.DevelopServer)
        {
            serverAddress = "192.168.1.160";
        }
        else if (serverType == ServerType.DemoServer)
        {
            serverAddress = "192.168.1.125";
        }
    }

    void Start () {
        if (isShowFrameUpdate)
        {
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
            SendPing();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isShowFrameUpdate)
        {
            m_FrameUpdate++;
            if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
            {
                m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
                m_FrameUpdate = 0;
                m_LastUpdateShowTime = Time.realtimeSinceStartup;
            }
        }
    }



    void SendPing()
    {
        ping = new Ping(serverAddress);
    }


    void OnGUI()
    {
        if (isShowFrameUpdate)
        {
            //FPS
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.background = null;    //设置背景填充  
            fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色  
            fontStyle.fontSize = 40;       //字体大小  
            GUI.Label(new Rect(Screen.width - 100, 0, 100, 100), "FPS: " + (int)m_FPS);


            //Ping
            GUI.color = Color.red;

            GUI.Label(new Rect(10, 0, 100, 20), "ping: " + delayTime.ToString() + "ms");

            if (null != ping && ping.isDone)
            {
                delayTime = ping.time;
                ping.DestroyPing();
                ping = null;
                Invoke("SendPing", 1.0F);//每秒Ping一次
            }


        }
    }
}
