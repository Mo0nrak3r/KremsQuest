using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float floatStrength;
    float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
        originalY + (Mathf.Sin(Time.time) * floatStrength),
        transform.position.z);
    }
}
