using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

protected Transform player;
private Rigidbody rb;

private float BulletSpeed = 70f;
string hitObject;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.rotation = player.rotation;
        transform.position = player.position + transform.right * 2;

        StartCoroutine(DestroyBullet());
    }


    void FixedUpdate()
    {
        rb.velocity = (transform.right * BulletSpeed);
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
