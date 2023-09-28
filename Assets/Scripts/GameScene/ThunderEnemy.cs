using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private Player player = null;
    [SerializeField] private UIControl uIControl = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());   
    }

    private IEnumerator Move()
    {
        while(true)
        {
            while(true)
            {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
                if (transform.position.y <= -85)
                {
                    transform.position = new Vector3(transform.position.x, -85, 0);
                    break;
                }
                yield return null;
            }

            yield return new WaitForSeconds(5f);

            while(true)
            {
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                if (transform.position.x <= 23)
                {
                    transform.position = new Vector3(23, transform.position.y, 0);
                    break;
                }
                yield return null;
            }

            yield return new WaitForSeconds(5f);

            while(true)
            {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                if (transform.position.y >= -45)
                {
                    transform.position = new Vector3(transform.position.x, -45, 0);
                    break;
                }
                yield return null;
            }

            yield return new WaitForSeconds(5f);

            while(true)
            {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                if (transform.position.x >= 42)
                {
                    transform.position = new Vector3(42, transform.position.y, 0);
                    break;
                }
                yield return null;
            }

            yield return new WaitForSeconds(5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            player.GetThunder = true;
            player.GetNum++;
            uIControl.GetItemUpdate();
            Destroy(this.gameObject);
        }
    }
}
