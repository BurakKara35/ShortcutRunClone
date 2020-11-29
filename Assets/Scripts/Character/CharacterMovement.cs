using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField] private float runSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpSpeed;

    private bool jumpedOnce = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Run()
    {
        rigidbody.velocity = transform.forward * Time.fixedDeltaTime * runSpeed;
    }

    public void TurnLeft()
    {
        transform.RotateAround(transform.position, transform.up, Time.fixedDeltaTime * -turnSpeed);
    }
    
    public void TurnRight()
    {
        transform.RotateAround(transform.position, transform.up, Time.fixedDeltaTime * turnSpeed);
    }

    public void Jump()
    {
        rigidbody.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
    }

    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
    }

    public bool JumpedOnce
    {
        get { return jumpedOnce; }
        set { jumpedOnce = value; }
    }
}
