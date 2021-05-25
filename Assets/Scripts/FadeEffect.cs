using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    int fadeInt;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        fadeInt = Animator.StringToHash("Fade");
        animator = GetComponent<Animator>();

        GameManager.registerFade(this);
        LoadManager.registerFade(this);
    }

    public void playFade()
    {
        animator.SetTrigger(fadeInt);
    }
}
