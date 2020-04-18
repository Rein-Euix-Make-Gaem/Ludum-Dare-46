using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Animator cameraAnimator;

    public void SmallShake()
    {
        cameraAnimator.SetTrigger("smallShake");
    }

    public void BigShake()
    {
        cameraAnimator.SetTrigger("bigShake");
    }
}
