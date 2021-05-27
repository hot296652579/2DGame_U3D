using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Audio Event Channel")]
public class AudioEventChanelSO:ScriptableObject
{
    public AudioPlayAction OnAudioPlayRequested;
    public AudioCueStopAction OnAudioStopRequested;
    public AudioCueFinishAction OnAudioFinishRequested;

    public void RaisePlayEvent(AudioObjectSO audioObjectSO)
    {
        if(OnAudioPlayRequested != null)
        {
            OnAudioPlayRequested.Invoke(audioObjectSO);
        }
        else
        {
            Debug.LogWarning("RaisePlay 声音未添加注册...");
        }
    }

    public bool RaiseStopEvent()
    {
        bool requestSucceed = false;
        if (OnAudioStopRequested != null)
        {
            requestSucceed = OnAudioStopRequested.Invoke();
        }
        else
        {
            Debug.LogWarning("RaiseStop 声音未添加注册...");
        }
        return requestSucceed;
    }

    public bool RaiseFinishEvent()
    {
        bool requestSucceed = false;
        if (OnAudioFinishRequested != null)
        {
            requestSucceed = OnAudioFinishRequested.Invoke();
        }
        else
        {
            Debug.LogWarning("RaiseFinish 声音未添加注册...");
        }
        return requestSucceed;
    }
}

public delegate void AudioPlayAction(AudioObjectSO audioObjectSO);
public delegate bool AudioCueStopAction();
public delegate bool AudioCueFinishAction();
