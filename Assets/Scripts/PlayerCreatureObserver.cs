using UnityEngine;
using UnityEngine.UI;

public class PlayerCreatureObserver : MonoBehaviour
{
    public CreatureAttitudeManager CreatureAttitudeManager;
    public Slider UpsetValueSlider;

    // Update is called once per frame
    void Update()
    {
        if (CreatureAttitudeManager == null)
        {
            return;
        }

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
}
