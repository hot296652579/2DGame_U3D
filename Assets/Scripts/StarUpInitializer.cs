using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class StarUpInitializer : MonoBehaviour
{
    [SerializeField] private GameSceneSO _thisSceneSO = default;
    [SerializeField] private GameSceneSO _persistentManagersSO = default;
    [SerializeField] private AssetReference _notifyColdStartupChannel = default;
    void Start()
    {
        if (!SceneManager.GetSceneByName(_persistentManagersSO.assetReference.editorAsset.name).isLoaded)
        {
            _persistentManagersSO.assetReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
        }
    }

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        _notifyColdStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoaded;
    }

    private void OnNotifyChannelLoaded(AsyncOperationHandle<LoadEventChannelSO> obj)
    {
        LoadEventChannelSO loadEventChannelSO = (LoadEventChannelSO)_notifyColdStartupChannel.Asset;
        loadEventChannelSO.RaiseEvent(_thisSceneSO);
    }

}
