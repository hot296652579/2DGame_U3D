using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameSceneSO _locationToLoad;
    [SerializeField]
    private bool _showLoadScreen = default;

    [SerializeField]
    private LoadEventChannelSO _startGameEvent = default;
    [SerializeField]
    private VoidEventChannelSO _startNewGameEvent = default;

    // Start is called before the first frame update
    void Start()
    {
        _startNewGameEvent.OnEventRaised += StartNewGame;
    }

    void StartNewGame()
    {
        _startGameEvent.RaiseEvent(_locationToLoad, _showLoadScreen);
    }
}
