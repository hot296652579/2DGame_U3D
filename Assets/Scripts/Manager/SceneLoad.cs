using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    [SerializeField]  private GameSceneSO gamePlayScene = default;

    [Header("Load Events")]
    [SerializeField] private LoadEventChannelSO _loadLocation = default;
    [SerializeField] private LoadEventChannelSO _coldStartupLocation = default;

    [Header("Broadcasting on")]
    [SerializeField] private BoolEventChannelSO _toggleLoadingScreen = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    private GameSceneSO _sceneToLoad;
    private GameSceneSO _currentLoadedScene;
    private bool _showLoadingScreen;

    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
    private AsyncOperationHandle<SceneInstance> gameManagerLoadingOpHandler;

    private SceneInstance sceneInstance = new SceneInstance();


    private void OnEnable()
    {
        _loadLocation.onLoadingRequested += LoadLocation;
        _coldStartupLocation.onLoadingRequested += LocationColdStartup;
    }

    private void OnDestroy()
    {
        _loadLocation.onLoadingRequested -= LoadLocation;
        _coldStartupLocation.onLoadingRequested -= LocationColdStartup;
    }

    void LoadLocation(GameSceneSO gameSceneso, bool showLoading)
    {
        _sceneToLoad = gameSceneso;
        _showLoadingScreen = showLoading;

        if(sceneInstance.Scene == null || !sceneInstance.Scene.isLoaded)
        {
            gameManagerLoadingOpHandler = gamePlayScene.assetReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            gameManagerLoadingOpHandler.Completed += OnGameplayMangersLoaded;
        }
        else
        {
            UnloadPreviousScene();
        }
    }

    private void LocationColdStartup(GameSceneSO currentlyOpenedLocation, bool showLoadingScreen)
    {
        _currentLoadedScene = currentlyOpenedLocation;

        if (_currentLoadedScene.sceneType == GameSceneSO.GameSceneType.Location)
        {
            //Gameplay managers is loaded synchronously
            gameManagerLoadingOpHandler = gamePlayScene.assetReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            gameManagerLoadingOpHandler.Completed += onLocationStartUpLoaded;
        }
    }

    private void onLocationStartUpLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        sceneInstance = gameManagerLoadingOpHandler.Result;

        StartGameplay();
    }

    void OnGameplayMangersLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        sceneInstance = gameManagerLoadingOpHandler.Result;
        UnloadPreviousScene();
    }

    void UnloadPreviousScene()
    {
        if (_currentLoadedScene != null) //would be null if the game was started in Initialisation
        {
            if (_currentLoadedScene.assetReference.OperationHandle.IsValid())
            {
                //Unload the scene through its AssetReference, i.e. through the Addressable system
                _currentLoadedScene.assetReference.UnLoadScene();
            }
#if UNITY_EDITOR
            else
            {
       
                SceneManager.UnloadSceneAsync(_currentLoadedScene.assetReference.editorAsset.name);
            }
#endif
        }

        LoadNewScene();
    }

    void LoadNewScene()
    {
        if (_showLoadingScreen)
        {
            _toggleLoadingScreen.RaiseEvent(true);
        }

        _loadingOperationHandle = _sceneToLoad.assetReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }

    void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _currentLoadedScene = _sceneToLoad;
        SetActiveScene();

        if (_showLoadingScreen)
        {
            _toggleLoadingScreen.RaiseEvent(false);
        }
    }

    void SetActiveScene()
    {
        Scene s = ((SceneInstance)_loadingOperationHandle.Result).Scene;
        SceneManager.SetActiveScene(s);

        LightProbes.TetrahedralizeAsync();

        StartGameplay();
    }

    private void StartGameplay()
    {
        _onSceneReady.RaiseEvent(); //Spawn system will spawn the PigChef
    }
}
