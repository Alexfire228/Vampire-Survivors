using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Explode : NetworkBehaviour
{
    [SerializeField] private WeaponStatsSO stats;

    private float timer;

    void Start()
    {
        timer = 0;

        EnemyStats enemyStats = null;
        for (int i = 0; i < MobSpawner.Instance.Enemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, MobSpawner.Instance.Enemies[i].transform.position) <= 3)
            {
                enemyStats = MobSpawner.Instance.Enemies[i].GetComponent<EnemyStats>();

                enemyStats.ChangeHP(stats.Damage);
            }
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            Destroy(this.gameObject);
        }
    }
}
