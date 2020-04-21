using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAttentionController : MonoBehaviour
{
    public Transform TVTarget;
    public Transform CameraTarget;

    private CreatureAttitudeManager cam;
    private Transform target;

    // Angular speed in radians per sec.
    public float speed = 1.0f;

    private void Start()
    {
        this.cam = GameObject.FindGameObjectWithTag("CreatureRoom").GetComponent<CreatureAttitudeManager>();

        this.target = this.cam.HasDistractions ? this.TVTarget : this.CameraTarget;
    }

    void Update()
    {
        this.target = this.cam.HasDistractions ? this.TVTarget : this.CameraTarget;

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
