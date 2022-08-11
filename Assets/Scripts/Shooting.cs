using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] shotComponents;
    public Text AmmoCounter;

    bool isDelayed;
    float shootingDelay = 0.3f;
    bool isReloaded = true;
    float reloadDelay = 1.5f;
    bool reload;
    int maxShots;
    int ammo;
    string weaponName;
    byte shotsCounter;

    void Start()
    {
        weaponName = gameObject.name;
        switch (weaponName)
        {
            case "pistol":
                maxShots = 8;
                break;
        }
    }

    void Update()
    {
        Shoot();
        Ammo();
    }

    void Shoot()
    {
        if ((Input.GetKey(KeyCode.R)) && (shotsCounter != 0)) reload = true;

        if ((shotsCounter >= maxShots) || (reload == true))
        {
            isReloaded = false;
            reloadDelay -= Time.deltaTime;
            Debug.ClearDeveloperConsole();
            print("Reload");
            if (reloadDelay < 0)
            {
                isReloaded = true;
                reloadDelay = 1.5f;
                shotsCounter = 0;
                print("Reloaded");
                reload = false;
            }
        }

        if (isDelayed == true)
        {
            shootingDelay -= Time.deltaTime;
            if ( shootingDelay < 0 )
            {
                isDelayed = false;
                shootingDelay = 0.3f;
            }
        }

        if(Input.GetKey(KeyCode.Mouse0) && (isDelayed == false) && (isReloaded == true))
        {
            isDelayed = true;
            Instantiate(shotComponents[0]);
            Instantiate(shotComponents[1]);
            Instantiate(shotComponents[2]);
            shotsCounter++;
        }

        ammo = maxShots - shotsCounter;
    }

    void Ammo()
    {
        AmmoCounter.text = string.Format( "{0}/{1}", ammo,maxShots);
    }
}
