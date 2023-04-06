using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours : Character
{
    [Header("===---Move Properties---===")]
    [SerializeField] private Vector3 InputVector;
    [SerializeField] private FloatingJoystick Joystick;

    public override void Start()
    {
        ChangeAnimation("Idle");
    }

    void Update()
    {
        InputVector.x = (Input.GetAxis("Horizontal") == 0) ? Joystick.Horizontal : Input.GetAxis("Horizontal");
        InputVector.z = (Input.GetAxis("Vertical") == 0) ? Joystick.Vertical : Input.GetAxis("Vertical");

        if (Vector3.Distance(InputVector, Vector3.zero) < 0.1f)
        {
            ChangeAnimation("Idle");
        }
        else
        {
            ChangeAnimation("Run");
            PlayerRotate();
        }

        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (canMove || InputVector.z < 0)
        {
            _rigidbody.velocity = InputVector * moveSpeed;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void PlayerRotate()
    {
        if (Vector3.Distance(InputVector, Vector3.zero) > 0.1f)
        {
            Quaternion toRotate = Quaternion.LookRotation(InputVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, moveSpeed);
        }
        else
        {
            transform.rotation = transform.rotation;
        }
    }
}
