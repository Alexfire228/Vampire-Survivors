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
        flyPoint = new Vector3(10,10,0);
        stats = statsSO;
        flyTime = 0f;

        starterPoint = transform.position;
        StartCoroutine(Fly());

    }

    void Update()
    {
        if (Vector3.Distance(starterPoint, flyPoint) <= 0.1f)
        {
            Instantiate(explode, flyPoint, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Fly()
    {
        while (flyTime < 2)
        {
            transform.position = Vector3.Lerp(starterPoint, flyPoint, flyTime / 2f);
            yield return new WaitForEndOfFrame();
            flyTime += Time.deltaTime;
        }
    }
}
