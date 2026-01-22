using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MeteoriteProjectile : NetworkBehaviour
{
    private WeaponStatsSO stats;

    private float flyTime;
    private Vector3 flyPoint;

    private Vector3 starterPoint;

    [SerializeField] private GameObject explode;
    public void Setup(Vector3 currpoint, WeaponStatsSO statsSO)
    {
        flyPoint = currpoint;
        stats = statsSO;
        flyTime = 0;

        starterPoint = transform.position;
    }

    void Update()
    {
        flyTime += Time.deltaTime;
        transform.position = Vector3.Lerp(starterPoint, flyPoint, 2f / flyTime);

        if (Vector3.Distance(starterPoint, flyPoint) <= 0.1f)
        {
            Instantiate(explode, flyPoint, Quaternion.identity);
        }
    }
}
