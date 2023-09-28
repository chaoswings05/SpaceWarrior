using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEnemy : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private UIControl uIControl = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            player.GetWind = true;
            player.GetNum++;
            uIControl.GetItemUpdate();
            Destroy(this.gameObject);
        }
    }
}
