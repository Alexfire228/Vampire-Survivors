using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SingleMob : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO stats;

    private Transform nearestPlayer;

    private SpriteRenderer spriteRenderer;

    private float speed;
    private float health;
    private float damage;

    void Start()
    {
        speed = stats.Speed;
        health = stats.MaxHealth;
        damage = stats.Damage;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        NearestPlayerServerRPC();
        transform.position = Vector3.MoveTowards(transform.position, nearestPlayer.position, Time.deltaTime * speed);

        if (nearestPlayer.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void NearestPlayerServerRPC()
    {
        var players = NetworkManager.Singleton.ConnectedClients;
        float minDistance = float.MaxValue;

        foreach (var player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.Value.PlayerObject.transform.position);

            if (distanceToPlayer < minDistance)
            {
                nearestPlayer = player.Value.PlayerObject.transform;
                minDistance = distanceToPlayer;
            }
        }
    }
}
