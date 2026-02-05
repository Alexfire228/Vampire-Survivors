using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons", fileName = "New Weapon")]
public class WeaponStatsSO : ScriptableObject
{
    public float Damage;
    public float Cooldown;
    public float DestroyDelay;
    public float FlyTime;
    public float Radius;
}
