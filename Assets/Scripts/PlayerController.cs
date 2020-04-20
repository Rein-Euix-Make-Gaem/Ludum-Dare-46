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


    public string JumpEvent = "";
    FMOD.Studio.EventInstance jumpSound;

    public bool IsCarryingPatch;
    public GameObject CarriedLargePatch;

    private bool jump = false;
    private float speed;
    private Vector3 direction;
    private bool grounded;
    private Rigidbody body;

    private void Start()
    {
        grounded = false;
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        body.useGravity = false;

        this.IsCarryingPatch = false;
        this.CarriedLargePatch.SetActive(false);

        jumpSound = FMODUnity.RuntimeManager.CreateInstance(JumpEvent);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Update()
    {
        CheckGround();

        if (grounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //if (GameManager.Instance.IsFirstPersonControllerEnabled)
        //{
        speed = Input.GetKey(KeyCode.LeftShift) && !this.IsCarryingPatch ? runSpeed : walkSpeed;

            var x = Input.GetAxisRaw("Horizontal");
            var z = Input.GetAxisRaw("Vertical");

            direction = new Vector3(x, 0, z);
            direction = Vector3.Normalize(direction);
        //}
        //else
        //{
        //    speed = 0;
        //}
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
            jump = false;
            jumpSound.start();

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
