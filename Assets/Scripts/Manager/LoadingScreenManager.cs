using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{

    [SerializeField] private BoolEventChannelSO _toggleLoadingScreen;

    public GameObject loadingInterface;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(_toggleLoadingScreen != null)
        {
            _toggleLoadingScreen.OnEventRaised += ToggleLoadingScreen;
        }
    }

    private void OnDisable()
    {
        if (_toggleLoadingScreen != null)
        {
            _toggleLoadingScreen.OnEventRaised -= ToggleLoadingScreen;
        }
    }

    void ToggleLoadingScreen(bool isBool)
    {
        loadingInterface.SetActive(isBool);
    }
}
