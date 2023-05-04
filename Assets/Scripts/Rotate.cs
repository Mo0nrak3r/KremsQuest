using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 _rotation;
    public float _speed;

    void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }
}
