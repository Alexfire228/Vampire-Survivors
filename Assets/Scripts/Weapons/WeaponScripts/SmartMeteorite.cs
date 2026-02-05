using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SmartMeteorite : PlayerWeapon
{
    [SerializeField] private int scanResolution;

    [SerializeField] private int deltaPoints;

    private Vector3[] spcoors;
    private int[] spenemies;

    private float size;

    private bool canStart;

    [SerializeField] private GameObject meteorite;
    [SerializeField] private WeaponStatsSO stats;
    public override void OnWeaponCDFinished()
    {
        canStart = true;
    }

    public override void OnWeaponDestroyed()
    {
        
    }

    private Vector3 FindBestPoint(string mode)
    {
        Vector3 bestpoint = Vector3.zero;
        Vector3 scanpoint = Vector3.zero;
        int pointenemies = 0;
        int enemiesrecord = 0;
        

        size = deltaPoints * (scanResolution - 1);
        Vector3 leftdown = transform.position - new Vector3(size/2, size/2, 0);
        
        for (int i = 0; i < scanResolution; i++)
        {
            for (int j = 0; j < scanResolution; j++)
            {
                scanpoint = leftdown + new Vector3(i * deltaPoints, j * deltaPoints, 0);

                if (Vector3.Distance(transform.position, scanpoint) > size/2f) //�������������� �����������
                {
                    continue;
                }
                
                switch (mode)
                {
                    case "Max enemies":
                        for (int k = 0; k < MobSpawner.Instance.Enemies.Count; k++)
                        {
                            if (Vector3.Distance(scanpoint, MobSpawner.Instance.Enemies[k].transform.position) <= 10)
                            {
                                pointenemies++;
                            }
                        }

                        if (enemiesrecord < pointenemies)
                        {
                            enemiesrecord = pointenemies;
                            bestpoint = scanpoint;
                        }

                        pointenemies = 0;
                        break;
                }
            }
        }

        

        return bestpoint;
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            GameObject temp = Instantiate(meteorite, new Vector3(10, 20, 0), Quaternion.identity);
            temp.GetComponent<NetworkObject>().Spawn();
            temp.GetComponent<MeteoriteProjectile>().Setup(FindBestPoint("Max enemies"), stats);
            Debug.Log(FindBestPoint("Max enemies"));
        }
    }
}
