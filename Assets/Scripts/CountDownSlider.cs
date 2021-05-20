using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownSlider : MonoBehaviour
{
    public float totalTime;
    public float playTime;
    public float burningTime;

    Text timeTxt;
    Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        playTime = totalTime;

        slider = GetComponent<Slider>();
        timeTxt = GetComponentInChildren<Text>();
        slider.maxValue = burningTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        playTime -= Time.deltaTime;
        if(playTime <= burningTime)
        {
            slider.value = playTime;

            int totalSceconds = Mathf.CeilToInt(playTime);

            string secondsPart = (totalSceconds % 60).ToString();
            string minsPart = Mathf.RoundToInt(totalSceconds / 60).ToString();
            //timeTxt.text = string.Format("{0}:{1}", minsPart, secondsPart);

            if (secondsPart.Length == 1)
                secondsPart = "0" + secondsPart;
            if (minsPart.Length == 1)
                minsPart = "0" + minsPart;

            timeTxt.text = string.Format("{0}:{1}", minsPart, secondsPart);
        }

        if(playTime <= 0f)
        {
            playTime = totalTime;
            //GameManager.GameOver();
        }
    }
}
