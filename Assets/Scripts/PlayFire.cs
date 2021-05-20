using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFire : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bulletPre;
    private Bullet bullet;
    public GameObject boomEft;
    public LayerMask groundLayer;
    public LineRenderer lineRender;

    // Update is called once per frame
    void Update()
    {
        //Fire();
    }

    public void Fire()
    {
        //if (Input.GetButtonDown("FireBullet"))
        //{
        //    StartCoroutine(FireAction());
        //}
        StartCoroutine(FireAction());
    }

    IEnumerator FireAction()
    {
        float dir = transform.localScale.x;
        Debug.Log(dir);
        Vector2 pos = firePoint.transform.position;
        Vector2 playDir;
        if (dir > 0f)
            playDir = Vector2.right;
        else
            playDir = Vector2.left;

        Vector3 maxDistance = transform.TransformDirection(playDir) * 100;
        RaycastHit2D hint = Physics2D.Raycast(pos, playDir, 100, groundLayer);
        Debug.DrawRay(pos, maxDistance, Color.green);

        if (hint)
        {
            EnemyHealth enemyHealth = hint.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(30);
                Instantiate(boomEft, hint.point, Quaternion.identity);

                lineRender.SetPosition(0, pos);
                lineRender.SetPosition(1, hint.point);
            }
        }
        else
        {
            lineRender.SetPosition(0, pos);
            lineRender.SetPosition(1, maxDistance);
        }

        lineRender.enabled = true;
        yield return new WaitForSeconds(0.02f);
        lineRender.enabled = false;
    }
}
