using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public GameObject deathPre;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Destroy(gameObject);
        Instantiate(deathPre, transform.position, transform.rotation);
        gameObject.SetActive(false);
        AudioManager.playDeathAudio();
    }
}
