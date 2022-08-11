using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotComponents : MonoBehaviour
{

    protected Transform shooter;
    protected Transform aim;
    private Rigidbody rb;

    private byte bulletSpeed = 140;
    private sbyte coefficient;
    private string weaponName;
    private string currentTag;

    void Awake()
    {
        currentTag = gameObject.tag;

        shooter = GameObject.FindWithTag("Shooter").transform;
        weaponName = GameObject.FindWithTag("Shooter").name;
        aim = GameObject.FindWithTag("RotatableTarget").transform;

        if (currentTag == "Bullet") rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        switch (weaponName)
        {
            case "pistol":
                coefficient = 3;
                break;
        }

        StartCoroutine(DestroyObj());

        transform.rotation = aim.rotation;
        transform.position = shooter.position + transform.forward * 2 + transform.up * 1 / coefficient;

    }


    void FixedUpdate()
    {
        if (currentTag == "Bullet") rb.velocity = (transform.forward * bulletSpeed);
    }

    IEnumerator DestroyObj()
    {
        switch (currentTag)
        {
            case "Bullet":
                yield return new WaitForSeconds(5);
                break;
            case "Smoke":
                yield return new WaitForSeconds(0.2f);
                break;
            case "Fire":
                yield return new WaitForSeconds(0.04f);
                break;
        }
                
        Destroy(gameObject);
    }

}
