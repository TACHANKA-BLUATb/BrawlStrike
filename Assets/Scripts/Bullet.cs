using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    string hitObject;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnCollisionEnter(UnityEngine.Collision hit)
    {
        hitObject = hit.gameObject.tag;
        Destroy(gameObject);
    }
}
