using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Incidents.Asteroids
{
    public class PowerDisabledIncident : Incident
    {
        public LightPuzzle puzzle;

        public override bool CanSpawn()
        {
            return GameManager.Instance.IsAsteroidFieldActive &&
                   GameManager.Instance.IsPowerActive;
        }

        public override void Spawn()
        {
            puzzle.Spawn();

            GameManager.Instance.SetPowerActive(false);
        }
    }
}
