using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public class EnemyTypeData
{
    public GameObject EnemyPrefab;
    public int count;
}

[System.Serializable]
public class WaveData
{
    public List<EnemyTypeData> EnemyTypes;
    public float Cost;
    public float SpawnDelay;
    public float WaveDelay;
}

public class MobSpawner : NetworkBehaviour
{
    private float coins;
    [SerializeField] private WaveData[] waves;

    void FixedUpdate()
    {
        
    }

    public override void OnNetworkSpawn()
    {

    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].Cost <= coins)
            {
                coins -= waves[i].Cost;

                for (int j = 0; j < waves[i].EnemyTypes.Count; j++)
                {
                    Instantiate(waves[i].EnemyTypes[j].EnemyPrefab, PlayerCamera.Instance.transform.posi);
                }
            }
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        //Homework: Сделать спавн в виде кольца, и волн.
    }
}
