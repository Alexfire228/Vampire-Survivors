using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMob", menuName = "Create mob")]

public class EnemyStatsSO : ScriptableObject
{
    public float Speed;
    public float MaxHealth;
    public float Damage;
    public float Mana;
    public float ManaResSpeed;
    public float SummonCost;
}
