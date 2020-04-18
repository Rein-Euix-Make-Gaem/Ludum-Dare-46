using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float gravity = 10;
    public float jumpHeight = 1f;
    public float maxVelocity = 6f;
    public float groundDistance = 1.1f;

    private bool grounded;
    private Rigidbody body;

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
            var speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var targetVelocity = new Vector3(x, 0, z);

            targetVelocity = Vector3.Normalize(targetVelocity);
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
            var jumpVelocity = new Vector3(body.velocity.x, jumpSpeed, body.velocity.z);

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
