using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotShotCheker : MonoBehaviour
{
    public Text Health;
    int _health;
    void Start()
    {
        _health = 100;
    }

    void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        Health.text = _health.ToString() + "/100";
    }

    void OnCollisionEnter(Collision bullet)
    {
    if (bullet.gameObject.tag == "Bullet")
    {
        _health -= 10;
    }


    }
}
