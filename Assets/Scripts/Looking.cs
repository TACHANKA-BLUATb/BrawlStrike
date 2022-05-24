using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
Transform target;

float RotationSpeed = 6.5f;

    void Start()
    {
        target = GameObject.FindWithTag("RotatableTarget").transform;
    }

    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }
}
