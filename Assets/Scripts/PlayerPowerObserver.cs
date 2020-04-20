using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerPowerObserver : MonoBehaviour
    {
        public LightController headLamp;

        private bool previousPowerState = true;

        private void Update()
        {
            var powerState = GameManager.Instance.IsPowerActive;

            if (powerState != previousPowerState)
            {
                Debug.Log($"headlamp {(powerState ? "off" : "on")}");
                headLamp.Toggle(!powerState);
            }

            previousPowerState = powerState;
        }
    }
}
