using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Generated;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

//下面这行为了取消使用WWW的警告，Unity2018以后推荐使用UnityWebRequest，处于兼容性考虑Demo依然使用WWW
#pragma warning disable CS0618
public class HotFixILManager : MonoBehaviour
{
    private static HotFixILManager instance;
    public static HotFixILManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Action UpdateEventHandler;
    public Action FixedUpdateEventHandler;
    public Action LateUpdateEventHandler;


    //AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
    //大家在正式项目中请全局只创建一个AppDomain
    private ILRuntime.Runtime.Enviorment.AppDomain appdomain;
    private System.IO.MemoryStream fs;
    private System.IO.MemoryStream p;

    private IStaticMethod start;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        StartCoroutine(LoadHotFixAssembly());

    }

    IEnumerator LoadHotFixAssembly()
    {
        //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        //正常项目中应该是自行从其他地方下载dll，或者打包在AssetBundle中读取，平时开发以及为了演示方便直接从StreammingAssets中读取，
        //正式发布的时候需要大家自行从其他地方读取dll

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
        //工程目录在Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
        //以下加载写法只为演示，并没有处理在编辑器切换到Android平台的读取，需要自行修改
#if UNITY_ANDROID
        WWW download = new WWW("http://192.168.50.226:8000/remote/" + "/HotFix_Project.dll");
        yield return download;
        File.WriteAllBytes(Application.persistentDataPath + "/HotFix_Project.dll", download.bytes);
        download.Dispose();

        WWW www = new WWW("file:///" + Application.persistentDataPath + "/HotFix_Project.dll");
#else
        //WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");
        var downloadRequest = AssetBundleManager.Instance.DownloadAssetBundleAsync("HotFix_Project.dll");
        yield return downloadRequest;
        GameUtility.SafeWriteAllBytes(AssetBundleUtility.GetPersistentDataPath() + "/HotFix_Project.dll", downloadRequest.bytes);
        downloadRequest.Dispose();

        WWW www = new WWW("file:///" + AssetBundleUtility.GetPersistentDataPath() + "/HotFix_Project.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
        /*
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        */
        fs = new MemoryStream(dll);
        //p = new MemoryStream(pdb);
        try
        {
            //appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            appdomain.LoadAssembly(fs, null, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册
        // 注册重定向函数
        
        // 注册委托
        RegistDelegate();
        
        //注册值类型绑定
        RegisterValueType();

        // 注册适配器

        //LitJson进行注册
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
        
        //初始化CLR绑定请放在初始化的最后一步！！
        //请在生成了绑定代码后解除下面这行的注释
        CLRBindings.Initialize(appdomain);
    }

    void OnHotFixLoaded()
    {
        //热更工程初始化方法调用
        this.start = new ILStaticMethod(this.appdomain, "HotFix_Project.HotFixInit", "Init", 0);
        this.start.Run();
        //appdomain.Invoke("HotFix_Project.HotFixInit", "Init", null, null);

    }
    void RegistDelegate()
    {
        DelegateManager manager = appdomain.DelegateManager;
        manager.RegisterMethodDelegate<bool>();
        manager.RegisterMethodDelegate<byte>();
        manager.RegisterMethodDelegate<sbyte>();
        manager.RegisterMethodDelegate<char>();
        manager.RegisterMethodDelegate<short>();
        manager.RegisterMethodDelegate<ushort>();
        manager.RegisterMethodDelegate<int>();
        manager.RegisterMethodDelegate<int,int>();
        manager.RegisterMethodDelegate<int, int,int>();
        manager.RegisterMethodDelegate<uint>();
        manager.RegisterMethodDelegate<long>();
        manager.RegisterMethodDelegate<ulong>();
        manager.RegisterMethodDelegate<float>();
        manager.RegisterMethodDelegate<double>();
        manager.RegisterMethodDelegate<string>();
        manager.RegisterMethodDelegate<object>();
        manager.RegisterMethodDelegate<Collider>();
        manager.RegisterMethodDelegate<Collision>();
        manager.RegisterMethodDelegate<BaseEventData>();
        manager.RegisterMethodDelegate<PointerEventData>();
        manager.RegisterMethodDelegate<UnityEngine.Object>();
        manager.RegisterMethodDelegate<GameObject>();

        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((action) => {
            return new UnityEngine.Events.UnityAction(() => {
                ((System.Action)action)();
            });
        });

    }

    void RegisterValueType()
    {
        appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
    }

    private void Update()
    {
        UpdateEventHandler?.Invoke();
    }

    private void FixedUpdate()
    {
        FixedUpdateEventHandler?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateEventHandler?.Invoke();
    }

    private void OnDestroy()
    {
        if (fs != null)
            fs.Close();
        if (p != null)
            p.Close();
        fs = null;
        p = null;
    }

}
