using UnityEngine;

namespace Assets.Scripts.Incidents
{
    public class AsteroidIncident : Incident
    {
        public Spawner asteroidSpawner;
        public float duration = 90;

        public int spawnRateMultiplier;

        private float time = 0;

        public override bool CanSpawn()
        {
            return GameManager.Instance.IsAsteroidSpawningEnabled &&
                !GameManager.Instance.IsAsteroidFieldActive;
        }

        public override void Spawn()
        {
            GameManager.Instance.SetAsteroidFieldActive(true);
            asteroidSpawner.spawnRateMultiplier = this.spawnRateMultiplier;
            time = 0;
        }

        private void Update()
        {
            var active = GameManager.Instance.IsAsteroidFieldActive;

            asteroidSpawner.spawnRateMultiplier = active ? this.spawnRateMultiplier : 1;

            if (active)
            {
                time += Time.deltaTime;

                if (time >= duration)
                {
                    asteroidSpawner.spawnRateMultiplier = 1;
                    GameManager.Instance.SetAsteroidFieldActive(false);
                    Debug.Log("asteroids finished");
                }
            }
        }

    }
}
