using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Event Channel")]
public class LoadEventChannelSO : ScriptableObject
{
    [TextArea] public string description;
    public UnityAction<GameSceneSO, bool> onLoadingRequested;

    public void RaiseEvent(GameSceneSO gameSceneso,bool showLoading = false)
    {
        if(onLoadingRequested != null)
        {
            onLoadingRequested.Invoke(gameSceneso, showLoading);
        }
        else
        {
            Debug.LogWarning("A Scene loading was requested ...");
        }
    }
}
