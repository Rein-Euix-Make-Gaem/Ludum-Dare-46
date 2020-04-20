using Doozy.Engine.Progress;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreatureObserver : MonoBehaviour
{
    public CreatureAttitudeManager CreatureAttitudeManager;
    public Progressor UpsetProgressor;

    // Update is called once per frame
    void Update()
    {
        if (CreatureAttitudeManager == null)
        {
            return;
        }

        this.UpsetProgressor.SetValue(this.CreatureAttitudeManager.CurrentUpsetValue);
    }
}
