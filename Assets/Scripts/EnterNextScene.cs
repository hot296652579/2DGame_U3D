using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    int playerId;
    
    public LoadManager loadManager;

    void Awake()
    {
        playerId = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerId)
        {
            loadManager.LoadNextScene();
        }
    }
}
