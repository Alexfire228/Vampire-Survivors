using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class NecromancerStateMachine : MonoBehaviour
{
    private EnemyStats enemyStats;
    public SummonState SummonState => summonState;
    private SummonState summonState;

    public MeleeState MeleeState => meleeState;
    private MeleeState meleeState;

    public AttackState AttackState => attackState;
    private AttackState attackState;

    private BaseState currState;

    [SerializeField] private GameObject mobPrefab;

    private bool startFlag = false;

    private Transform nearestPlayer;
    public Transform NearestPlayer => nearestPlayer;

    public void ChangeState(BaseState state)
    {
        if (currState != null)
            currState.Exit();

        currState = state;

        currState.Enter();

        Debug.Log(currState);
    }

    void FixedUpdate()
    {
        if (!startFlag)
        {
            enemyStats = GetComponent<EnemyStats>();
            summonState = new SummonState(this, enemyStats, mobPrefab);
            meleeState = new MeleeState(this, enemyStats);
            attackState = new AttackState(this, enemyStats);
            ChangeState(summonState);
            startFlag = true;
        }
    }

    void Update()
    {
        if (currState != null)
        {
            currState.LogicUpdate();
        }

        NearestPlayerServerRPC();
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
