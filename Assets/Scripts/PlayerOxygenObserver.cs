using Doozy.Engine.Progress;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygenObserver : MonoBehaviour
{
    public Progressor OxygenProgressor;
    public GameObject SuffocationMask;

    // Update is called once per frame
    void Update()
    {
        this.OxygenProgressor.SetValue(GameManager.Instance.CurrentOxygen);
        MeshRenderer temp = this.SuffocationMask.GetComponent<MeshRenderer>();

        if(GameManager.Instance.CurrentOxygen <= 0)
        {
            float percent = (GameManager.Instance.ElapsedSuffocationTime / GameManager.Instance.SuffocationTime)*2;
            temp.material.SetColor("_BaseColor", new Color(temp.material.color.r, temp.material.color.g, temp.material.color.b, percent));
        }
        else
        {
            temp.material.SetColor("_BaseColor", new Color(temp.material.color.r, temp.material.color.g, temp.material.color.b, 0));
        }
    }

    public void SuffocateDeath()
    {
        Animator anim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        anim.enabled = true;
        anim.SetTrigger("suffocationDeath");
    }
}
