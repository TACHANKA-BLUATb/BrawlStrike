using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public NetworkHandler network;

    public MouseFollow _follow;

    private float MoveForce = 8f;
    private float MovementX;
    private float MovementZ;

    private Vector3 RotationVector;

    private void Update()
    {
        PlayerwMoveKeyboard();
        //RotateCharacter(RotationVector);
    }

    protected void PlayerwMoveKeyboard()
    {
        MovementX = Input.GetAxisRaw("Horizontal");
        MovementZ = Input.GetAxisRaw("Vertical");

        if (MovementX != 0 && MovementZ != 0)
            MoveForce = 6f;
        else
            MoveForce = 8f;

        var vector = new Vector3(MovementX, 0, MovementZ) * Time.deltaTime * MoveForce;

        var isPlayerMove = (MovementX != 0) | (MovementZ != 0);

        transform.position += vector;

        if (network != null && network.getWsHandler != null && network.getWsHandler.getWsConnection != null &&
            isPlayerMove)
            network.getWsHandler.sendMessage(transform.position, transform.rotation, network.currentPlayerId());
    }

    public void RotateCharacter(Vector3 _direction)
    {
        /*var targetForward = Vector3.RotateTowards(transform.forward, _direction.normalized, 10f * Time.deltaTime, .1f);
        var _newRotation = Quaternion.LookRotation(targetForward);
        transform.rotation = _newRotation;*/
        if (_follow != null)
        {
            transform.rotation = Quaternion.AngleAxis(transform.rotation.y, _follow.GetRotationVector());

            var quaternion = _follow.GetRotation();

            var yAngle = quaternion.y;
            var yAngleCurrent = transform.rotation.y;

            if (Math.Abs(yAngle) - Math.Abs(yAngleCurrent) > 0.8) transform.rotation = quaternion;
            if (Math.Abs(yAngleCurrent) - Math.Abs(yAngle) > 0.8) transform.rotation = quaternion;
        }
    }
}