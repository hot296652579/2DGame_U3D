using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnAttackPressBtn : MonoBehaviour
{
    private PlayFire playFire;
    public Button button;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playFire = player.GetComponent<PlayFire>();
        button.onClick.AddListener(() =>
        {
            playFire.Fire();
        });
    }
}
