using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHealth : MonoBehaviour
{
    int trapLayerId;
    public GameObject deathPre;

    // Start is called before the first frame update
    void Start()
    {
        trapLayerId = LayerMask.NameToLayer("Traps");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == trapLayerId)
        {
            Instantiate(deathPre, transform.position, transform.rotation);
            gameObject.SetActive(false);

            AudioManager.playDeathAudio();

            GameManager.GameOver();
        }
    }
}
