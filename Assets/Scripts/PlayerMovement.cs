﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

protected Rigidbody myBody;
private Vector3 TempPos;
private Vector3 RotationVector;

private float MoveForce = 8f;
private float MovementX;
private float MovementZ;
float tiltAngle = 60.0f;

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
	    transform.position += new Vector3(MovementX, 0, 0) * Time.deltaTime * MoveForce;

        MovementZ = Input.GetAxisRaw("Vertical");
	    transform.position += new Vector3(0, 0, MovementZ) * Time.deltaTime * MoveForce; 


    }

}
