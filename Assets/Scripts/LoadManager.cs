using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    private static LoadManager current;
    public GameObject LoadSlider;
    FadeEffect fadeEft;

    private Slider slider;
    private Text progressTxt;

    // Start is called before the first frame update
    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);

        slider = LoadSlider.GetComponentInChildren<Slider>();
        progressTxt = LoadSlider.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        LoadSlider.gameObject.SetActive(false);
    }

    public static void registerFade(FadeEffect eft)
    {
        current.fadeEft = eft;
    }

    public void LoadNextScene()
    {
        current.fadeEft.playFade();
        current.Invoke("LoadScene", 1.2f);
    }

    void LoadScene()
    {
        LoadSlider.gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsynce());
    }

    IEnumerator LoadSceneAsynce()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;
            progressTxt.text = "Loading progress: " + (asyncLoad.progress * 100) + "%";
            if (asyncLoad.progress >= 0.9f)
            {
                slider.value = 1;
                progressTxt.text = "Loading progress:100%";
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
