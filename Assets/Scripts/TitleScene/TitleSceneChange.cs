using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneChange : MonoBehaviour
{
    [SerializeField] private Image fadeImage = null;
    private bool FadeOut = false;
    private bool FadeIn = false;
    [SerializeField] private float fadeSpeed = 1f;
    private int fadeInTimes = 0;

    // Update is called once per frame
    void Update()
    {
        if (FadeOut)
        {
            fadeImage.color -= new Color(0,0,0,fadeSpeed * Time.deltaTime);

            if (fadeImage.color.a <= 0)
            {
                fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,0);
                FadeOut = false;
                FadeIn = true;
                if (fadeInTimes >= 4)
                {
                    FadeOut = false;
                    fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,0);
                    SceneManager.LoadScene("GameScene");
                }
            }
        }
        if (FadeIn)
        {
            fadeImage.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
            if (fadeImage.color.a >= 1)
            {
                fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,1);
                FadeIn = false;
                FadeOut = true;
                fadeInTimes++;
            }
        }
    }

    public void StartSceneChange()
    {
        SoundManager.Instance.PlaySE(1);
        FadeIn = true;
    }
}
