using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO stats;
    
    public EnemyStatsSO Stats => stats;

    private float hp;
    private float speed;
    private float damage;
    private float mana;

    public float Mana => mana;

    void Start()
    {
        hp = stats.MaxHealth;
        speed = stats.Speed;
        damage = stats.Damage;
        mana = stats.Mana;
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
        if (hp - value <= 0)
        {
            Destroy(this);
        }

        hp -= value;
    }
}
