using UnityEngine;

namespace Assets.Scripts.Incidents
{
    public class AsteroidIncident : Incident
    {
        public Spawner asteroidSpawner;
        public float duration = 90;

        private float time = 0;

        public override bool CanSpawn()
        {
            return GameManager.Instance.IsAsteroidSpawningEnabled &&
                !GameManager.Instance.IsAsteroidFieldActive;
        }

        public override void Spawn()
        {
            GameManager.Instance.SetAsteroidFieldActive(true);
            asteroidSpawner.enabled = true;
            time = 0;
        }

        private void Update()
        {
            var active = GameManager.Instance.IsAsteroidFieldActive;

            asteroidSpawner.enabled = active;

            if (active)
            {
                time += Time.deltaTime;

                if (time >= duration)
                {
                    asteroidSpawner.enabled = false;
                    GameManager.Instance.SetAsteroidFieldActive(false);
                    Debug.Log("asteroids finished");
                }
            }
        }

    }
}
