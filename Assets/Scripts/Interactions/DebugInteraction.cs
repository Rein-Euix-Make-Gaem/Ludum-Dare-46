using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class DebugInteraction : Interactable
    {
        public override string description => "DEBUG";

        protected override void OnInteract(ref InteractionEvent ev)
        {
            Debug.Log("interaction");
        }
    }
}
