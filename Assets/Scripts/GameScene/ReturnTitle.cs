using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnTitle : MonoBehaviour
{
    [SerializeField] private Image fadeImage = null;
    [SerializeField] private float fadeSpeed = 1f;
    private bool FadeOut = false;
    private bool FadeIn = false;

    // Update is called once per frame
    void Update()
    {
        if (FadeIn)
        {
            fadeImage.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
            if (fadeImage.color.a >= 1)
            {
                fadeImage.color = new Color(0,0,0,1);
                FadeIn = false;
                FadeOut = true;
                SceneManager.LoadScene("TitleScene");
            }
        }
        if (FadeOut)
        {
            fadeImage.color -= new Color(0,0,0,fadeSpeed * Time.deltaTime);

            if (fadeImage.color.a <= 0)
            {
                fadeImage.color = new Color(0,0,0,0);
                FadeOut = false;
            }
        }
    }

    public void StartSceneChange()
    {
        FadeIn = true;
    }
}
