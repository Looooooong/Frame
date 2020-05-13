using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour {

    // 固定的一个时间间隔
    private float time_delta = 0.5f;
    // Time.realtimeSinceStartup: 指的是我们当前从启动开始到现在运行的时间，单位(s)
    private float prev_time = 0.0f; // 上一次统计FPS的时间;
    private float fps = 0.0f; // 计算出来的FPS的值;
    private int i_frames = 0; // 累计我们刷新的帧数;

    // GUI显示;
    private GUIStyle style;

    void Awake() {
        // 假设CPU 100% 工作的状态下FPS 300，
        // 当你设置了这个以后，他就维持在60FPS左右，不会继续冲高;
        // -1, 游戏引擎就会不段的刷新我们的画面，有多高，刷多高; 60FPS左右;
        Application.targetFrameRate = 60;
    }

	// Use this for initialization
	void Start () {
        this.prev_time = Time.realtimeSinceStartup;
        this.style = new GUIStyle();
        this.style.fontSize = 15;
        this.style.normal.textColor = new Color(255, 255, 255);
	}

    void OnGUI() {
        GUI.Label(new Rect(0, Screen.height - 20, 200, 200), "FPS:" + this.fps.ToString("f2"), this.style);
    }
	// Update is called once per frame
    // 每次游戏刷新的时候就会调用;
	void Update () {
        this.i_frames ++;

        if (Time.realtimeSinceStartup >= this.prev_time + this.time_delta) {
            this.fps = ((float)this.i_frames) / (Time.realtimeSinceStartup - this.prev_time);
            this.prev_time = Time.realtimeSinceStartup;
            this.i_frames = 0; // 重新累积我们的FPS
        }
	}
}
