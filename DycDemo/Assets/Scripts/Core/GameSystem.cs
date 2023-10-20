
public abstract class GameSystem
{
    public virtual void Init() { }
    /// <summary>
    /// ��¼�ɹ�
    /// </summary>
    /// <param name="init_">�Ƿ���Ҫ��ʼ����������</param>
    public virtual void OnLogin(bool init_) { }
   
    public virtual void OnStart() { }
  
    public virtual void OnLogout() { }
  
    public virtual void UnInit() { }
  
    public virtual void Save(long CurTimestamp_) { }
   
    public virtual void OnDestroy() { }
}
