using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Quaternion _rotation;

    private Vector3 RotationVector;

    private void Update()
    {
        MouseRotation();
    }

    private void MouseRotation()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        _rotation = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = _rotation;
    }
    
}