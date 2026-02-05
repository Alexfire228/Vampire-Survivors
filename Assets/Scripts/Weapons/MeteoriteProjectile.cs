using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MeteoriteProjectile : NetworkBehaviour
{
    private WeaponStatsSO stats;
    private WeaponStatsSO explodeStats;

    private float flyTime;
    private Vector3 flyPoint;

    private Vector3 starterPoint;

    [SerializeField] private GameObject explode;
    public void Setup(Vector3 currpoint, WeaponStatsSO statsSO, WeaponStatsSO explode)
    {
        flyPoint = currpoint;
        stats = statsSO;
        explodeStats = explode;

        if (explodeStats == null)
        {
            Debug.Log("Alert!");
        }

        flyTime = 0f;

        starterPoint = transform.position;
        StartCoroutine(Fly());

    }

    

    IEnumerator Fly()
    {
        while (flyTime < 2)
        {
            transform.position = Vector3.Lerp(starterPoint, flyPoint, flyTime / 2f);
            yield return new WaitForEndOfFrame();
            flyTime += Time.deltaTime;
        }
        Explode instexplode = Instantiate(explode, flyPoint, Quaternion.identity).GetComponent<Explode>();
        instexplode.Setup(explodeStats);
        Destroy(this.gameObject);
    }
}
