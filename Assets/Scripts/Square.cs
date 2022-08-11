using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
[SerializeField]
private GameObject[] BrokenObj;

    void OnCollisionEnter()
    {
        Instantiate(BrokenObj[0]);
        Destroy(gameObject);
    }
}
