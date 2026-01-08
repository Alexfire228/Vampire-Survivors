using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
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
    private float currWaveCost;

    private Transform playerTransform;

    private List<NetworkObject> enemies = new List<NetworkObject>();

    public static MobSpawner Instance;

    private bool spawnFlag;

    void Awake()
    {
        spawnFlag = false;
        Instance = this;
    }

    void FixedUpdate()
    {
        coins += 0.05f + currWaveCost * 1.03f;

        if (!spawnFlag)
        {
            StartCoroutine(SpawnWave());
            spawnFlag = true;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playerTransform = NetworkManager.Singleton.ConnectedClients[0].PlayerObject.transform;
            currWaveCost = 1;
            coins = 0;
            StartCoroutine(SpawnWave());
            //PI();
        }
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < waves.Length; i++)
        {
            if (coins >= waves[i].Cost)
            {
                coins -= waves[i].Cost;
                currWaveCost += waves[i].Cost;

                for (int j = 0; j < waves[i].EnemyTypes.Count; j++)
                {
                    for (int k = 0; k < waves[i].EnemyTypes[j].count; k++)
                    {
                        SpawnEnemy(waves[i].EnemyTypes[j].EnemyPrefab, playerTransform.position);
                        yield return new WaitForSeconds(waves[i].SpawnDelay);
                    }
                }

                yield return new WaitForSeconds(waves[i].WaveDelay);
            }
        }

        spawnFlag = false;
    }

    public void SpawnEnemy(GameObject enemyPrefab, Vector3 pos)
    {
        float rmin = 7.5f;
        float rmax = 15;

        Vector3 currdist = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);

        if (Vector3.Distance(pos + currdist, pos) > rmin && Vector3.Distance(pos + currdist, pos) < rmax)
        {
            enemies.Add(Instantiate(enemyPrefab, pos + currdist, Quaternion.identity).GetComponent<NetworkObject>());
        }

        //Homework: рефакторинг кода, чтобы было через цикл, и выбиралась автоматически с помощью генератора случайных чисел (done)
        //Homework: вычислить число p (эксперементально), S = пи * (r * r), добавить список врагов, * - создать шифр (самый надёжный)
        /*Homework(пи)*/
        //Debug.Log("S = " + Mathf.PI * (rmax * rmax));


    }

    private void PI()
    {
        //Homework от 4.12
        float x1 = playerTransform.position.x - 20;
        float x2 = playerTransform.position.x - 15;
        float y1 = playerTransform.position.y + 5;
        float y2 = playerTransform.position.y;

        int r = 10;

        float shootincircle = 0;
        float shootinsquare = 0;

        for (int i = 0; i < 100000; i++)
        {
            Vector3 shoot = new Vector3(Random.Range(playerTransform.position.x - 25, playerTransform.position.x + 25), Random.Range(playerTransform.position.y - 30, playerTransform.position.y + 30), 0);

            if (shoot.x >= x1 && shoot.x <= x2 && shoot.y <= y1 && shoot.y >= y2)
            {
                shootinsquare++;
                Debug.Log("Попал в квадрат!");
            }

            else if (Vector3.Distance(playerTransform.position, shoot) <= r)
            {
                shootincircle++;
                Debug.Log("Попал в круг!");
            }
        }

        float S = 25 * (shootincircle / shootinsquare);

        Debug.Log("Обстрел завершён! S круга = " + S);
        Debug.Log("Пи = " + S / (r * r));

    }
}
