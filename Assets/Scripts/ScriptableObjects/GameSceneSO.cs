using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

public class GameSceneso :ScriptableObject
{
    public GameSceneType sceneType;
    public AssetReference assetReference;

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
