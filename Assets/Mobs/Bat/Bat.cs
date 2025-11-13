using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO stats;

    private float speed;
    private float health;
    private float damage;

    void Start()
    {
        speed = stats.Speed;
        health = stats.MaxHealth;
        damage = stats.Damage;
    }
}
