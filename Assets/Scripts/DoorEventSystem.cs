using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static DoorEventSystem Instance;

    public event Action onDoorOpen;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Instance = null;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void onOpenTheDoor()
    {
        if (onDoorOpen != null)
            onDoorOpen();
    }

}
