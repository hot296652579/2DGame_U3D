using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update 
    private static GameManager current;
    FadeEffect fadeEft;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);
    }

    public static void registerFade(FadeEffect eft)
    {
        current.fadeEft = eft;
    }

    public static void GameOver()
    {
        current.fadeEft.playFade();
        current.Invoke("ResetLoadScene", 1.2f);
    }

    void ResetLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
