using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenSquare : MonoBehaviour
{
private Transform Origin;

    void Start()
    {
        Origin = GameObject.FindWithTag("Square").transform;
        transform.position = Origin.position;
    }
}
