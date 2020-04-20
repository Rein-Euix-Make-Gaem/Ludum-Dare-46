using Assets.Scripts;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Spawnable[] items;

    public float minSpawnInterval = 30f;
    public float spawnChance = 0.1f;
    public float spawnInterval = 90f;

    public int spawnRateMultiplier;

    private float time;
    private float timeSinceLastSpawn;
    private float baseSpawnChance;
    private int actualSpawnRateMultiplier;

    private void Start()
    {
        this.baseSpawnChance = this.spawnInterval;
    }

    void Update()
    {
        var deltaTime = Time.deltaTime;

        time += deltaTime;
        timeSinceLastSpawn += deltaTime;

        if (time >= spawnInterval)
        {
            SpawnRandom();
            time = 0;
        }
    }

    bool CanSpawn(Spawnable spawnable)
    {        
        if (!spawnable.CanSpawn())
        {
            return false;
        }

        var chance = Random.value;

        return (chance <= spawnChance * actualSpawnRateMultiplier || timeSinceLastSpawn >= minSpawnInterval);
    }

    public void SpawnRandom(bool force = false)
    {
        var index = Random.Range(0, items.Length);
        Spawn(index, force);
    }

    public void Spawn(int index, bool force = false)
    {
        // Index 2 is the power systems going offline
        if(index < 2) 
        {
            this.actualSpawnRateMultiplier = this.spawnRateMultiplier;
        }
        else
        {
            this.actualSpawnRateMultiplier = 1;
        }

        var spawnable = items[index];

        if (!force && !CanSpawn(spawnable))
        {
            return;
        }

        spawnable.Spawn();
        timeSinceLastSpawn = 0;

        BroadcastMessage("OnSpawn", spawnable,
            SendMessageOptions.DontRequireReceiver);
    }
}
