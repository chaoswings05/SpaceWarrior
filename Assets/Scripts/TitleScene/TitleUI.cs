using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Text Title = null;
    [SerializeField] private Text clickToStart = null;
    [SerializeField] private float fadeSpeed = 1f;
    private bool FadeOut = false;
    private bool FadeIn = false;
    private bool GameStart = false;
    [SerializeField] private CameraShake cameraShake = null;

    // Start is called before the first frame update
    void Start()
    {
        FadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeOut)
        {
            clickToStart.color -= new Color(0,0,0,fadeSpeed * Time.deltaTime);

            if (clickToStart.color.a <= 0)
            {
                clickToStart.color = new Color(clickToStart.color.r,clickToStart.color.g,clickToStart.color.b,0);
                FadeOut = false;
                FadeIn = true;
            }
        }
        if (FadeIn)
        {
            clickToStart.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
            if (clickToStart.color.a >= 1)
            {
                clickToStart.color = new Color(clickToStart.color.r,clickToStart.color.g,clickToStart.color.b,1);
                FadeIn = false;
                FadeOut = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            FadeIn = false;
            FadeOut = false;
            GameStart = true;
        }

        if (GameStart)
        {
            Title.color -= new Color(0,0,0,fadeSpeed * Time.deltaTime);
            clickToStart.color -= new Color(0,0,0,fadeSpeed * Time.deltaTime);

            if (Title.color.a <= 0 && clickToStart.color.a <= 0)
            {
                Title.color = new Color(Title.color.r,Title.color.g,Title.color.b,0);
                clickToStart.color = new Color(clickToStart.color.r,clickToStart.color.g,clickToStart.color.b,0);
                GameStart = false;
                cameraShake.Shake(0.5f, 0.5f);
                SoundManager.Instance.PlaySE(0);
            }
        }
    }
}
