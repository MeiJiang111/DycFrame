
public abstract class GameSystem
{
    public virtual void Init() { }
    /// <summary>
    /// 登录成功
    /// </summary>
    /// <param name="init_">是否需要初始化缓存数据</param>
    public virtual void OnLogin(bool init_) { }
   
    public virtual void OnStart() { }
  
    public virtual void OnLogout() { }
  
    public virtual void UnInit() { }
  
    public virtual void Save(long CurTimestamp_) { }
   
    public virtual void OnDestroy() { }
}
