using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public NetworkHandler network;

    public Transform body;
    public Transform target;

    private float MoveForce = 8f;
    private float MovementX;
    private float MovementZ;

    private Vector3 RotationVector;

    private void Start()
    {
        body = GameObject.FindWithTag("Shooter").transform;
        target = GameObject.FindWithTag("RotatableTarget").transform;
    }

    private void Update()
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

        var vector = new Vector3(MovementX, 0, MovementZ) * Time.deltaTime * MoveForce;

        var isPlayerMove = (MovementX != 0) | (MovementZ != 0);

        if (network.status && isPlayerMove) network.sendMessage(vector);
        transform.position += vector;

        RotationVector = new Vector3(MovementX, 0, MovementZ);
    }

    public void RotateCharacter(Vector3 _direction)
    {
        var Angle = Quaternion.Angle(transform.rotation, body.rotation);

        if (Angle > 70)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 3f * Time.deltaTime);
        }
        else
        {
            Vector3 targetForward = Vector3.RotateTowards(transform.forward, _direction.normalized, 10f * Time.deltaTime,.1f);
            Quaternion _newRotation = Quaternion.LookRotation(targetForward);
            transform.rotation = _newRotation;
        }
        
        //  if ((_follow != null) & (MovementX == 0) & (MovementZ == 0))
        //  {
        //     transform.rotation = Quaternion.AngleAxis(transform.rotation.y, _follow.GetRotationVector())
        //     var quaternion = _follow.GetRotation();
        //     var yAngle = quaternion.y;
        //     var yAngleCurrent = transform.rotation.y;

        //     if (Math.Abs(yAngle) - Math.Abs(yAngleCurrent) > 0.7) transform.rotation = quaternion;
        //     if (Math.Abs(yAngleCurrent) - Math.Abs(yAngle) > 0.7) transform.rotation = quaternion;
    }
}
