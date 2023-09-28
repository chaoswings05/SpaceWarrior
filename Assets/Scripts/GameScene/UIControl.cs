using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private GameObject Thunder = null;
    [SerializeField] private GameObject Fire = null;
    [SerializeField] private GameObject Water = null;
    [SerializeField] private GameObject Wind = null;
    [SerializeField] private GameObject DeadEndUI = null;
    [SerializeField] private GameObject BadEndUI = null;
    [SerializeField] private GameObject NormalEndUI = null;
    [SerializeField] private GameObject GoodEndUI = null;
    private bool GameEnd = false;
    [SerializeField] private ReturnTitle returnTitle = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnd && Input.GetMouseButtonDown(0))
        {
            returnTitle.StartSceneChange();
        }
    }

    public void GetItemUpdate()
    {
        if (player.GetThunder)
        {
            Thunder.SetActive(true);
        }
        if (player.GetFire)
        {
            Fire.SetActive(true);
        }
        if (player.GetWater)
        {
            Water.SetActive(true);
        }
        if (player.GetWind)
        {
            Wind.SetActive(true);
        }
    }

    public void ShowDeadEndUI()
    {
        GameEnd = true;
        DeadEndUI.SetActive(true);
    }

    public void ShowBadEndUI()
    {
        GameEnd = true;
        BadEndUI.SetActive(true);
    }

    public void ShowNormalEndUI()
    {
        GameEnd = true;
        NormalEndUI.SetActive(true);
    }

    public void ShowGoodEndUI()
    {
        GameEnd = true;
        GoodEndUI.SetActive(true);
    }
}
