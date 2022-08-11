using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
Transform target;
Transform Legs;

float RotationSpeed = 6.5f;

    void Start()
    {
        target = GameObject.FindWithTag("RotatableTarget").transform;
        //Legs = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime * 3);

        // var quaternion = Legs.rotation;
        // float quaternionControl = quaternion.y;

        // if (transform.rotation.y > quaternion.y + 0.3f)
        // {
        //     if (quaternionControl == quaternion.y) quaternion.y += 0.3f;   

        //     transform.rotation = quaternion;
        // }
        // if (transform.rotation.y < quaternion.y - 0.3f)
        // {
        //     if (quaternionControl == quaternion.y) quaternion.y -= 0.3f;

        //     transform.rotation = quaternion;
        // }
    }
}
