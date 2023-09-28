using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBlackHole : MonoBehaviour
{
    private bool appear = false;
    [SerializeField] private TitleSceneChange titleSceneChange = null;

    // Update is called once per frame
    void Update()
    {
        if (appear)
        {
            transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            if (transform.localScale.x > 0.7f)
            {
                transform.localScale = new Vector3(0.7f,0.7f,0.7f);
                appear = false;
                titleSceneChange.StartSceneChange();
            }
        }
    }

    public void StartAppear()
    {
        appear = true;
    }
}
