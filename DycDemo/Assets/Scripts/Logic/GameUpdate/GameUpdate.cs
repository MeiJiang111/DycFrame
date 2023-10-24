using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameUpdate:MonoSingleton<GameUpdate>
{
    public enum UpdateState
    {
        None,
        Init,                      //初始化
        VerifyVersion,             //验证
        VerifyVersionSuccess,
        Download,                  //下载
        Finish,                    //结束
        Failed,                    //失败
    }

    public Action<UpdateState> UpdateStateChangedEvent;
    public Action<float> DownLoadProcessChangeEvent;

    UpdateState _state;
    string _lastName;
    string _lastErr;
   
    List<string> updateCatalogs;
    Coroutine updateCoroution;

    public UpdateState CurState
    {
        get 
        {
            return _state; 
        }

        set
        {
            _state = value;
            UpdateStateChangedEvent?.Invoke(_state);
        }
    }

    protected override void Awake()
    {
        LogUtil.Log("GameUpdate Awake");

        _state = UpdateState.None;
        updateCatalogs = new List<string>();
    }

    /// <summary>
    /// 游戏更新
    /// </summary>
    /// <param name="update_"></param>
    public void StartGameUpdate(bool update_ = true)
    {
        LogUtil.Log("GameUpdate StartGameUpdate == " + update_);
#if UNITY_EDITOR
        if (!update_)
        {
            CurState = UpdateState.Finish;
            //编辑器下可以跳过更新
            UpdateFinished();
            return;
        }
#endif

        ResourceManager.Instance.AddressableErrorEvent += OnAddressableErrored;
        updateCoroution = StartCoroutine(StartGameUpdateImple());
    }

    private void OnAddressableErrored(AsyncOperationHandle arg1, Exception arg2)
    {
        _lastName = arg1.DebugName;
        _lastErr = arg2.ToString();
        LogUtil.LogFormat($"_lastName == {_lastName} _lastErr == {_lastErr}");
    }

    IEnumerator StartGameUpdateImple()
    {
        CurState = UpdateState.Init;

        var initHandle = Addressables.InitializeAsync();
        yield return initHandle;

        if (!string.IsNullOrEmpty(_lastName))
        {
            CurState = UpdateState.Failed;
            LogUtil.LogErrorFormat("[GameUpdate]: int faild! {0} ", _lastErr);
            StopCoroutine(updateCoroution);
        }
        yield return new WaitForEndOfFrame();

        CurState = UpdateState.VerifyVersion;
        var handler = Addressables.CheckForCatalogUpdates(false);
        LogUtil.Log("GameUpdate StartGameUpdateImple handler == " + handler.Status);
        yield return handler;

        if (handler.Status != AsyncOperationStatus.Succeeded ||
           (!string.IsNullOrEmpty(_lastName)))
        {
            CurState = UpdateState.Failed;
            LogUtil.LogErrorFormat("[GameUpdate]: CheckForCatalogUpdates faild! {0} ", _lastErr);
            StopCoroutine(updateCoroution);
        }
        yield return new WaitForEndOfFrame();
       
        updateCatalogs = handler.Result;
        Addressables.Release(handler);
        if (updateCatalogs.Count > 0)
        {
            CurState = UpdateState.Download;
            updateCoroution = StartCoroutine(StartDownload());
        }
        else
        {
            CurState = UpdateState.Finish;
            UpdateFinished();
        }
    }

    public void UpdateFinished()
    {
        StartCoroutine(GameInitialize.Instance.EnterGame());
    }

    IEnumerator StartDownload()
    {
        yield return null;
       
        var updateHandler = Addressables.UpdateCatalogs(updateCatalogs, false);
        yield return updateHandler;
       
        if (updateHandler.Status != AsyncOperationStatus.Succeeded ||
            (!string.IsNullOrEmpty(_lastName)))
        {
            CurState = UpdateState.Failed;
            LogUtil.LogErrorFormat("[GameUpdate]: UpdateCatalogs faild! {0} ", _lastErr);
            StopCoroutine(updateCoroution);
        }
        yield return new WaitForEndOfFrame();
       
        CurState = UpdateState.VerifyVersionSuccess;
        var locators = updateHandler.Result;
        foreach (var locator in locators)
        {
            var downloadHandle = Addressables.DownloadDependenciesAsync(locator.Keys, Addressables.MergeMode.Union);
            while (!downloadHandle.IsDone)
            {
                DownLoadProcessChangeEvent?.Invoke(downloadHandle.PercentComplete);
                yield return new WaitForEndOfFrame();
            }
            Addressables.Release(downloadHandle);
        }
        CurState = UpdateState.Finish;
        Addressables.Release(updateHandler);
        DownLoadProcessChangeEvent?.Invoke(1);
        yield return null;
        UpdateFinished();
    }
}
