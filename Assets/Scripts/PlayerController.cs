using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 6f;
    [SerializeField] public float gravity = 10;
    [SerializeField] public float jumpHeight = 1f;
    [SerializeField] public float maxVelocity = 6f;
    [SerializeField] public float groundDistance = 1.1f;

    private bool grounded;
    private Rigidbody body;
    private Vector3 velocity;

    private void Start()
    {
        grounded = false;
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        body.useGravity = false;
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Update()
    {
        CheckGround();
    }

    private void Move()
    {
        if (grounded)
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var targetVelocity = new Vector3(x, 0, z);

            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            var velocity = body.velocity;
            var delta = targetVelocity - velocity;

            delta.x = Mathf.Clamp(delta.x, -maxVelocity, maxVelocity);
            delta.z = Mathf.Clamp(delta.z, -maxVelocity, maxVelocity);
            delta.y = 0;

            body.AddForce(delta, ForceMode.VelocityChange);
        }

        var gravityForce = new Vector3(0, -gravity * body.mass, 0);

        body.AddForce(gravityForce);
        
    }

    private void Jump()
    {
        if (!grounded)
        {
            return;
        }

        if (Input.GetButton("Jump"))
        {
            var jumpSpeed = CalculateJumpSpeed();
            var jumpVelocity = new Vector3(velocity.x, jumpSpeed, velocity.z);

            body.velocity = jumpVelocity;
        }
    }
    
    private float CalculateJumpSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    private void CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.blue);

        grounded = Physics.SphereCast(
            new Ray(transform.position, Vector3.down), 0.5f, groundDistance);
    }
}
