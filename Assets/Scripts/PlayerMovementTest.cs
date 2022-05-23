using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
protected Rigidbody myBody;
private Vector3 TempPos;

private float MoveForce = 7f;
private float MovementX;
private float MovementZ;

void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

void Update()
    {
        PlayerMoveKeyboard();
    }

protected void PlayerMoveKeyboard()
{
	MovementX = Input.GetAxisRaw("Horizontal");
    MovementZ = Input.GetAxisRaw("Vertical");
	myBody.velocity = (transform.right * MovementZ + transform.forward * -MovementX) * MoveForce;
    if ((MovementX != 0) && (MovementZ != 0))
    {
        myBody.velocity = (transform.right * MovementZ + transform.forward * -MovementX) * (MoveForce - 2);
    }
}
}
