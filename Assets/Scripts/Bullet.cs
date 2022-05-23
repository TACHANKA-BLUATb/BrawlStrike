using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

protected Transform shooter;
private Rigidbody rb;

private float BulletSpeed = 70f;
string hitObject;

    void Awake()
    {
        shooter = GameObject.FindWithTag("Shooter").transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.rotation = shooter.rotation;
        transform.position = shooter.position + transform.forward * 2;

        StartCoroutine(DestroyBullet());
    }


    void FixedUpdate()
    {
        rb.velocity = (transform.forward * BulletSpeed);
    }

    void OnCollisionEnter(UnityEngine.Collision hit)
    {
        hitObject = hit.gameObject.tag;  
        Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
