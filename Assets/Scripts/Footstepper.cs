using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepper : MonoBehaviour
{

    public string inputSound;
    bool playerismoving;
    public float walkingspeed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01 ||
            Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01 ){
                // Debug.Log("I'm moving!");
                playerismoving = true;
            }
        else {
            // Debug.Log("I'm standing still!");
            playerismoving = false;
        }
    }

    void CallFootsteps(){
        if (playerismoving){
            FMODUnity.RuntimeManager.PlayOneShot (inputSound);
        }
    }


}
