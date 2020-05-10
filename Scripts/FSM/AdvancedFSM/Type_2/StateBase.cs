/// <summary>
/// 状态基础类
/// </summary>
public class StateBase<T>
{
    //状态ID
    public int ID { get; set; }

    //状态所属者
    public T owner;

    //当前关联的状态机
    public StateMachine<T> machine;

    public StateBase(int id)
    {
        this.ID = id;
    }

    public StateBase(int id,T o)
    {
        this.ID = id;
        this.owner = o;
    }
        


    //给子类提供方法
    public virtual void OnEnter(params object[] args) { }
    public virtual void OnStay(params object[] args) { }
    public virtual void OnExit(params object[] args) { }
}
