using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannelSO _startNewGameEvent;

    public void clickNewGameBtn()
    {
        _startNewGameEvent.RaiseEvent();
    }
}
