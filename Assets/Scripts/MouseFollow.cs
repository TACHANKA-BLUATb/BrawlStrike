using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    Transform player;
    Vector3 TempPos;

    void Start ()
    {
        player = GameObject.FindWithTag("Legs").transform;
    }

    void Update () 
    {
        MouseRotation();
        Position();
    }

    private void MouseRotation() 
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    void Position()
    {
        TempPos = player.position;
        TempPos.y = player.position.y + 2;

        transform.position = TempPos;
    }
}
