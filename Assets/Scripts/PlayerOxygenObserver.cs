using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygenObserver : MonoBehaviour
{
    public Slider OxygenSlider;

    // Update is called once per frame

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        this.OxygenSlider.value = GameManager.Instance.CurrentOxygen;

        if (GameManager.Instance.CurrentOxygen <= 0)
        {
            this.OxygenSlider.gameObject.SetActive(false);
        }
        else
        {
            this.OxygenSlider.gameObject.SetActive(true);
        }
    }
}
