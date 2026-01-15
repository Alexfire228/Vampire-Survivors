using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SmartMeteorite : PlayerWeapon
{
    [SerializeField] private int scanResolution;

    [SerializeField] private int deltaPoints;

    private float size;
    public override void OnWeaponCDFinished()
    {
        
    }

    public override void OnWeaponDestroyed()
    {
        
    }

    private Vector3 FindBestPoint(string mode)
    {
        Vector2 bestpoint = Vector2.zero;
        Vector3 scanpoint = Vector3.zero;
        int pointenemies = 0;

        size = deltaPoints * (scanResolution - 1);
        Vector3 leftdown = transform.position - new Vector3(size/2, size/2, 0);
        
        for (int i = 0; i < scanResolution; i++)
        {
            for (int j = 0; j < scanResolution; j++)
            {
                scanpoint = leftdown + new Vector3(i * deltaPoints, j * deltaPoints, 0);

                if (Vector3.Distance(transform.position, scanpoint) > size/2) //Оптимизировать попробовать
                {
                    continue;
                }
                
                switch (mode)
                {
                    case "Max enemies":
                        for (int k = 0; k < MobSpawner.Instance.Enemies.Count; k++)
                        {
                            if (Vector3.Distance(scanpoint, MobSpawner.Instance.Enemies[k].transform.position) <= deltaPoints)
                            {
                                pointenemies++;
                            }
                        }

                        break;
                }
            }
        }

        return bestpoint;
    }

    void Update()
    {
        transform.position = FindBestPoint("Max enemies");
    }
}
