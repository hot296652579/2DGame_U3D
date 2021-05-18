using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.Invoke("destroyMyself", 0.5f);
    }

    void destroyMyself()
    {
        Destroy(gameObject);
    }

}
