using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreatureObserver : MonoBehaviour
{
    public CreatureAttitudeManager CreatureAttitudeManager;
    public Slider UpsetValueSlider;
    public Camera CreatureCamera;

    private bool inCreatureRoom;

    // Start is called before the first frame update
    void Start()
    {
        this.inCreatureRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpsetValueSlider.value = this.CreatureAttitudeManager.CurrentUpsetValue;
        
        if(this.CreatureAttitudeManager.CurrentUpsetValue <= 0)
        {
            this.UpsetValueSlider.gameObject.SetActive(false);
        }
        else
        {
            this.UpsetValueSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("CreatureRoom"))
        {
            this.inCreatureRoom = true;
            this.CreatureCamera.enabled = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("CreatureRoom"))
        {
            this.inCreatureRoom = false;
            this.CreatureCamera.enabled = true;
        }
    }
}
