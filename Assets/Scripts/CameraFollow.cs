using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
private Transform player;
private Vector3 TempPos;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; 
    }

    void Update()
    {
    TempPos = transform.position;

    TempPos.x = player.position.x;
    TempPos.z = player.position.z -7;

    transform.position = TempPos;
    }
}
