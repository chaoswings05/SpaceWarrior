using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,0,spinSpeed* Time.deltaTime);
    }
}
