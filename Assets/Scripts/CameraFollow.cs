using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
private Transform target;
private Vector3 TempPos;

    void Start()
    {
        target = GameObject.FindWithTag("CameraTarget").transform;
    }

    void Update()
    {
    TempPos = transform.position;

    TempPos.x = target.position.x;
    TempPos.z = target.position.z - 10;
    TempPos.y = target.position.y + 18;

    transform.position = TempPos;
    }
}
