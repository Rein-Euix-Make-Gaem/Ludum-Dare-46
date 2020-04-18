using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class CubeInteraction : ToggleInteraction
    {
        public GameObject target;

        protected override void OnInteract(ref InteractionEvent ev)
        {
            base.OnInteract(ref ev);

            if (target != null)
            {
                var renderer = GetComponent<MeshRenderer>();

                if (renderer != null)
                {
                    renderer.material.color = toggled ? Color.red : Color.white;
                }
            }

        }
    }
}
