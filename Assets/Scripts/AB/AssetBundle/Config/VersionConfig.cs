using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FileVersionInfo
{
    public string File;
    public string MD5;
    public long Size;
}

/// <summary>
/// 保存ab文件信息,版本校验使用
/// </summary>
public class VersionConfig
{
    public string Version;

    public long TotalSize;

    public Dictionary<string, FileVersionInfo> FileInfoDict = new Dictionary<string, FileVersionInfo>();
}

/// <summary>
/// 版本配置信息
/// </summary>
public class VersionInfo
{
    public string MainVersion;
    public string SubVersion;
    public string ResVersion;
}

/// <summary>
/// 网络地址配置信息
/// </summary>
public class NetInfo
{
    public string Host;
    public string Voice;
}