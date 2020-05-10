public enum ResourceType
{
    AS = 0,


    None = 99,
}


/// <summary>
/// 存放各个音效的类型
/// </summary>
public enum AudioClipType
{

    PistolShoot = 0,
    LaserShoot,
    LaserStart,
    LaserEnd,
    EnmeyShoot,
    BG_1,
    BG_2,


    None = 99,
}

/// <summary>
/// 设备分类
/// </summary>
public enum DeviceType
{
    Head,
    LeftHand,
    RightHand,
    Other
}


/// <summary>
/// 绘制曲线点的模式
/// </summary>
public enum BezierControlPointMode
{
    Free,
    Aligned,
    Mirrored
}

/// <summary>
/// 运动模式
/// </summary>
public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong
}

/// <summary>
/// 注册事件的类型
/// </summary>
public enum MG_EventType
{
    Test,
    EnemyNum,
    PlayerHpBar,
    PointsHUD,
    HUD,
}

/// <summary>
/// VR设备的厂家
/// </summary>
public enum VRDevice
{
    HTCVIVE,
    OCULUS,
}
