using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class LightInteraction : ToggleInteraction
    {
        public LightController lightController;
        public float intensity = 10f;

        protected override void OnInteract(ref InteractionEvent ev)
        {
            base.OnInteract(ref ev);

            Toggle(toggled);
        }

        public void Toggle(bool value)
        {
            toggled = value;
            lightController.intensity = value ? intensity : 0f;

        }
    }
}
