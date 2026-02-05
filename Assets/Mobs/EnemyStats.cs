using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyStats : NetworkBehaviour
{
    [SerializeField] private EnemyStatsSO stats;
    
    public EnemyStatsSO Stats => stats;

    private float hp;
    private float speed;
    private float damage;
    private float mana;

    public float Mana => mana;

    private NetworkObject networkObject;

    [SerializeField] private GameObject[] weapons;
    private int random;
    void Start()
    {
        hp = stats.MaxHealth;
        speed = stats.Speed;
        damage = stats.Damage;
        mana = stats.Mana;
        random = Random.Range(1, 10);
        networkObject = GetComponent<NetworkObject>();
    }

    public bool ChangeMana(float value)
    {
        if (mana - value >= 0)
        {
            mana -= value;
            return true;
        }

        else
            return false;
    }

    public void ChangeHP(float value)
    {
        if (hp - value < 0)
        {
            Debug.Log("Враг уничтожен");

            if (random == 1)
            {

            }

            MobSpawner.Instance.DestroyEnemy(networkObject);
        }

        hp -= value;
    }
}
