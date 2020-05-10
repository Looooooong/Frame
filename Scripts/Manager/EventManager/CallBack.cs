/// <summary>
/// 存放事件委托的定义
/// </summary>

public delegate void CallBack();
public delegate void CallBack<T>(T arg0);
public delegate void CallBack<T, X>(T arg0, X arg1);
public delegate void CallBack<T, X, Y>(T arg0, X arg1, Y arg2);