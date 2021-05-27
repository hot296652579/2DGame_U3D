using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

public class GameSceneSO : ScriptableObject
{
    public GameSceneType sceneType;
    public AssetReference assetReference;
    public AudioObjectSO musicTrack;

    public enum GameSceneType
    {
        Location,
        Menu,
        Initialisation,
        PersistentManagers,
        Gameplay,
        Art
    }
}