using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public abstract class PlayerWeapon : NetworkBehaviour
{
    protected WeaponStatsSO weaponStats;

    public virtual void Setup(WeaponStatsSO stats)
    {
        weaponStats = stats;
    }

    public abstract void OnWeaponCDFinished();

    public abstract void OnWeaponDestroyed();
}
