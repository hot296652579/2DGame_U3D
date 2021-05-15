using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    int playerId;
    public GameObject orbEffetPre;

    // Start is called before the first frame update
    void Start()
    {
        playerId = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerId)
        {
            Instantiate(orbEffetPre, transform.position, transform.rotation);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            AudioManager.playOrbAudio();
        }
    }
}
