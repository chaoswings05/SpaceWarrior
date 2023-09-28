using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage = null;
    [SerializeField] private float fadeSpeed = 1f;
    private bool FadeOut = false;
    private bool FadeIn = false;
    [SerializeField] private GameObject stage = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject blackHole = null;
    private string Ending = "";
    [SerializeField] private UIControl uIControl = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeIn)
        {
            fadeImage.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
            if (fadeImage.color.a >= 1)
            {
                fadeImage.color = new Color(0,0,0,1);
                switch(Ending)
                {
                    case "Bad":
                        stage.SetActive(false);
                        player.SetActive(false);
                        uIControl.ShowBadEndUI();
                        break;

                    case "Normal":
                        player.SetActive(false);
                        blackHole.SetActive(false);
                        uIControl.ShowNormalEndUI();
                        break;

                    case "Good":
                        blackHole.SetActive(false);
                        uIControl.ShowGoodEndUI();
                        break;
                }
                FadeIn = false;
                FadeOut = true;
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

    public void StartFade(string End)
    {
        Ending = End;
        FadeIn = true;
    }
}
