using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

private Vector3 RotationVector;

private float MoveForce = 8f;
private float MovementX;
private float MovementZ;


    void Update()
    {
        PlayerMoveKeyboard();
        RotateCharacter(RotationVector);
    }

    protected void PlayerMoveKeyboard()
    {
	    MovementX = Input.GetAxisRaw("Horizontal");
        MovementZ = Input.GetAxisRaw("Vertical");

        if (MovementX != 0 && MovementZ != 0)
            MoveForce = 6f;            
        else
            MoveForce = 8f;
        
	    transform.position += new Vector3(MovementX, 0, MovementZ) * Time.deltaTime * MoveForce;
        
        RotationVector = new Vector3(MovementX, 0, MovementZ);

    }
    public void RotateCharacter(Vector3 _direction)
    {
        Vector3 targetForward = Vector3.RotateTowards(transform.forward, _direction.normalized, 10f * Time.deltaTime,.1f);
        Quaternion _newRotation = Quaternion.LookRotation(targetForward);
        transform.rotation = _newRotation;
    }
}
