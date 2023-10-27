using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoSingleton<AudioSourceManager>
{
    public event Action<string> BGMChangeEvent;

    public AudioSource bgmAs;
    public AudioSource effectAs;

    string _bgm;
    public string CurBGM
    {
        get { return _bgm; }
        set
        {
            _bgm = value;
            BGMChangeEvent?.Invoke(_bgm);
        }
    }

    Dictionary<string, AudioClip> audioCache;

    protected override void Awake()
    {
        base.Awake();
        audioCache = new Dictionary<string, AudioClip>();

        SceneManager.Instance.SceneMgrStartLoadingNewSceneEvent += OnStartLoadingNewLevel;
        SceneManager.Instance.SceneMgrPreStartEvent += OnLevelPreStart;
    }

    private void OnStartLoadingNewLevel(string level_)
    {
        bgmAs.Stop();
    }

    private void OnLevelPreStart()
    {
        if (!string.IsNullOrEmpty(_bgm))
            PlayCurrBGM();
    }

    private void PlayCurrBGM(bool fadeIn_ = true)
    {
        bgmAs.loop = true;
        if (audioCache.TryGetValue(_bgm, out AudioClip clip_))
        {
            PlayAudioClip(bgmAs, clip_, fadeIn_);
        }
        else
        {
            ResourceManager.Instance.LoadAsset<AudioClip>(_bgm, (ac_, param_) =>
            {
                audioCache.Add(_bgm, ac_);
                PlayAudioClip(bgmAs, ac_, fadeIn_);
            }, (name_) => { LogUtil.LogErrorFormat("AudioClip {0} load faild!", name_); });
        }
    }

    void PlayAudioClip(AudioSource as_, AudioClip ac_, bool fadeIn_ = true)
    {
        as_.clip = ac_;
        if (fadeIn_)
        {
            as_.volume = 0;
            StartCoroutine(ChangeAudioVolume(as_, 0.1f, 1));
        }
        else
            as_.volume = 1;

        as_.Play();
    }

    IEnumerator ChangeAudioVolume(AudioSource bgmAs, float v, float target)
    {
        yield return null;
        while (bgmAs.volume != target)
        {
            bgmAs.volume = Mathf.Max(target, bgmAs.volume + v);
            yield return new WaitForSeconds(.1f);
        }
    }

    public bool PlayAudioClip(string Acname_, Action<AudioClip> OnLoaded = null)
    {
        if (audioCache.TryGetValue(Acname_, out AudioClip clip_))
        {
            OnLoaded?.Invoke(clip_);
            return true;
        }
        ResourceManager.Instance.LoadAsset<AudioClip>(Acname_, (ac_, param_) =>
        {
            audioCache.Add(Acname_, ac_);
            OnLoaded?.Invoke(ac_);
        }, (name_) => { LogUtil.LogErrorFormat("AudioClip {0} load faild!", name_); });
        return false;
    }

    public void PlayEffectVoice(string voiceName_)
    {
        if (audioCache.TryGetValue(voiceName_, out AudioClip clip_))
        {
            PlayAudioClip(effectAs, clip_, false);
        }
        else
        {
            ResourceManager.Instance.LoadAsset<AudioClip>(voiceName_, (ac_, param_) =>
            {
                audioCache.Add(voiceName_, ac_);
                PlayAudioClip(effectAs, ac_, false);
            }, (name_) => { LogUtil.LogErrorFormat("AudioClip {0} load faild!", name_); });
        }
    }
}
