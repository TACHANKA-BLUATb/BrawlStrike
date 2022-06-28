using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float BulletSpeed = 70f;
    protected Transform aim;
    private string hitObject;
    private Rigidbody rb;

    protected Transform shooter;

    private void Awake()
    {
        shooter = GameObject.FindWithTag("Shooter").transform;
        aim = GameObject.FindWithTag("RotatableTarget").transform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.rotation = aim.rotation;
        transform.position = shooter.position + transform.forward * 2;

        StartCoroutine(DestroyBullet());
    }


    private void FixedUpdate()
    {
        rb.velocity = transform.forward * BulletSpeed;
    }

    private void OnCollisionEnter(Collision hit)
    {
        hitObject = hit.gameObject.tag;
        Destroy(gameObject);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}