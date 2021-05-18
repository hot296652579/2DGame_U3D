using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rg2D;
    public float speed = 20;
    private int enemyId;
    public GameObject boomEft;

    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
        enemyId = LayerMask.NameToLayer("Enemy");

        float dir = PlayerPrefs.GetFloat("playDir");
        rg2D.velocity = dir * transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == enemyId)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(30f);

            Instantiate(boomEft, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
