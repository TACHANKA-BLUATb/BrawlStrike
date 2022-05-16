using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

    void Update () {
        MouseRotation();
    }

    private void MouseRotation() {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
