using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    public GameObject player = null;
    private Vector3 direction = Vector3.zero;
    private Vector3 originalPos = Vector3.zero;
    [SerializeField] private float moveSpeed = 1f;
    private bool Moving = false;
    [SerializeField] private Transform stick = null;
    private bool Pulling = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            transform.position += direction * moveSpeed * Time.deltaTime * 5;
            stick.localScale += new Vector3(Time.deltaTime * 10, 0, 0);
        }

        if (Pulling)
        {
            stick.localScale -= new Vector3(Time.deltaTime * 10, 0, 0);
            player.transform.position += direction * moveSpeed * Time.deltaTime * 5;

            if (stick.localScale.x <= 0.1f)
            {
                stick.localScale = new Vector3(0.1f, 0.5f, 0.5f);
                player.transform.position = stick.transform.position;
                player.GetComponent<Player>().ResetRb();
                Destroy(this.gameObject);
            }
        }
    }

    public void SetVector(Vector3 shotForward, GameObject owner)
    {
        player = owner;
        float rad = Mathf.Atan2(shotForward.y, shotForward.x);
        float degree = rad * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,degree);

        direction = shotForward;
        Moving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Moving = false;
            Pulling = true;
        }

        if (other.gameObject.CompareTag("Damage"))
        {
            player.GetComponent<Player>().ResetRb();
            Destroy(this.gameObject);
        }
    }
}
