using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interactions
{
    public class LightPuzzleInteraction : Interactable
    {
        public LightPuzzle puzzle;
        public LightInteraction puzzleLight;
        public LeverInteraction puzzleLever;

        protected override void OnInteract(ref InteractionEvent ev)
        {
            puzzle.Interacted(this);
        }

        public void Toggle(bool value)
        {
            puzzleLight.Toggle(value);
            puzzleLever.Toggle(value);
        }
    }
}
