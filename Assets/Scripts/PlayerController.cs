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
    public float groundDistance = 0.5f;
    public string jumpEvent = "event:/Jump";
    public string stepEvent = "event:/Step";
    public bool IsCarryingPatch;
    public GameObject CarriedLargePatch;
    public float groundDetectionRadius = 0.5f;
    public Vector3[] groundingTaps;
    
    private FMOD.Studio.EventInstance jumpSound;
    private FMOD.Studio.EventInstance stepSound;
    private bool jump;
    private float speed;
    private Vector3 direction;
    private Rigidbody body;
    private float stepTimer;

    [SerializeField]
    private bool grounded;

    private void Start()
    {
        grounded = false;
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        body.useGravity = false;

        this.IsCarryingPatch = false;
        this.CarriedLargePatch.SetActive(false);

        jumpSound = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);
        stepSound = FMODUnity.RuntimeManager.CreateInstance(stepEvent);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Footsteps()
    {
        var moving = direction.sqrMagnitude >= (0.001 * 0.001);

        if (moving)
        {
            stepTimer += Time.deltaTime;

            var strideDuration = 1f / speed;
            var stepsPerStride = 3;

            if (stepTimer >= strideDuration * stepsPerStride)
            {
                stepTimer = 0f;
                FMODUnity.RuntimeManager.PlayOneShot(stepEvent);
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    private void Update()
    {
        CheckGround();

        if (grounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        speed = Input.GetKey(KeyCode.LeftShift) && !this.IsCarryingPatch ? runSpeed : walkSpeed;

        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        direction = new Vector3(x, 0, z);
        direction = Vector3.Normalize(direction);

        Footsteps();
    }

    private void Move()
    {
        if (grounded)
        {
            var targetVelocity = transform.TransformDirection(direction);
            targetVelocity *= speed;

            var velocity = body.velocity;
            var delta = targetVelocity - velocity;

            delta.x = Mathf.Clamp(delta.x, -maxVelocity, maxVelocity);
            delta.z = Mathf.Clamp(delta.z, -maxVelocity, maxVelocity);
            delta.y = 0;

            body.AddForce(delta, ForceMode.Impulse);
        }

        var gravityForce = new Vector3(0, -gravity * body.mass, 0);

        body.AddForce(gravityForce);
    }

    private void Jump()
    {
        if (jump)
        {
            var jumpSpeed = CalculateJumpSpeed();
            var jumpVelocity = new Vector3(body.velocity.x, jumpSpeed, body.velocity.z);

            body.velocity = jumpVelocity;
            jump = false;
        }
    }

    private float CalculateJumpSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    private void CheckGround()
    {
        var wasGrounded = grounded;
        var count = groundingTaps.Length;

        for (var i = 0; i < count; i++)
        {
            var tap = groundingTaps[i];
            var checkPosition = transform.position + tap;

            Debug.DrawRay(checkPosition, Vector3.down * groundDistance, Color.red);

            grounded = Physics.Raycast(new Ray(checkPosition, Vector3.down), groundDistance);
            
            if (grounded)
            {
                break;
            }
        }
       
        if (!wasGrounded && grounded)
        {
            jumpSound.start();
            // HACK: avoids delay in jump sound
            jumpSound.setTimelinePosition(260);
        }
    }

    public void PickupLargePatch()
    {
        this.IsCarryingPatch = true;
        this.CarriedLargePatch.SetActive(true);

        FMODUnity.RuntimeManager.PlayOneShot("event:/PickUp");
    }

    public void DropLargePatch()
    {
        this.IsCarryingPatch = false;
        this.CarriedLargePatch.SetActive(false);

        FMODUnity.RuntimeManager.PlayOneShot("event:/PickUp");
    }
}
