using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Explode : NetworkBehaviour
{
    private WeaponStatsSO stats;

    private float timer;

    public void Setup(WeaponStatsSO statsSO)
    {
        timer = 0;
        stats = statsSO;

        if (stats == null)
        {
            Debug.Log("Alert!");
        }

        EnemyStats enemyStats = null;
        for (int i = 0; i < MobSpawner.Instance.Enemies.Count; i++)
        {
            enemyStats = MobSpawner.Instance.Enemies[i].GetComponent<EnemyStats>();

            if (Vector3.Distance(transform.position, MobSpawner.Instance.Enemies[i].transform.position) < stats.Radius && enemyStats != null)
            {
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
