using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
[SerializeField]
private GameObject[] bullet;

bool interval = false;
float shootingDelay = 0.1f;

    void Start()
    {

    }

    void Update()
    {
       Shoot();
    }

    void Shoot()
    {
        if (interval == true)
        {
            shootingDelay -= Time.deltaTime;
            if ( shootingDelay < 0 )
            {
                interval = false;
                shootingDelay = 0.1f;
            }
        }

        if(Input.GetKey(KeyCode.Mouse0) && (interval == false))
        {
            interval = true;
            Instantiate(bullet[0]);
        }
    }
}
