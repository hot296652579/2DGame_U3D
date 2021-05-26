using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToNextLevel : MonoBehaviour
{
    int playerId;

    [SerializeField] private GameSceneSO nextLevelScene = default;
    [SerializeField] private bool _showLoadScreen = default;
    // Start is called before the first frame update

    [SerializeField] private LoadEventChannelSO loadEventChannelSO = default;
    void Start()
    {
        playerId = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerId)
        {
            loadEventChannelSO.RaiseEvent(nextLevelScene, _showLoadScreen);
        }
    }
}
