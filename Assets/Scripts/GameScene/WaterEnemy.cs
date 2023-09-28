using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemy : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private UIControl uIControl = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            player.GetWater = true;
            player.GetNum++;
            uIControl.GetItemUpdate();
            Destroy(this.gameObject);
        }
    }
}
