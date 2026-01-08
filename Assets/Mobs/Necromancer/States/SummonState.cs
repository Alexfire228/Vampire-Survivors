using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonState : BaseState
{
    private Animator animator;

    private EnemyStats enemyStats;

    private GameObject summonMob;

    private float manaResTimer;
    private float summonTimer;
    public SummonState(NecromancerStateMachine sm, EnemyStats stats, GameObject mobPrefab)
    {
        stateMachine = sm;
        enemyStats = stats;
        animator = stateMachine.GetComponent<Animator>();
        summonMob = mobPrefab;
    }

    public override void Enter()
    {
        animator.SetBool("isSummon", true);
    }

    public override void Exit()
    {
        animator.SetBool("isSummon", false);
    }

    public override void LogicUpdate()
    {
        manaResTimer += Time.deltaTime;
        summonTimer += Time.deltaTime;

        if (manaResTimer >= 2)
        {
            enemyStats.ChangeMana(enemyStats.Stats.ManaResSpeed);
            manaResTimer = 0;
        }

        if (summonTimer >= 5)
        {
            enemyStats.ChangeMana(enemyStats.Stats.SummonCost);
            summonTimer = 0;
            MobSpawner.Instance.SpawnEnemy(summonMob, stateMachine.transform.position);
        }

        if (enemyStats.Mana < enemyStats.Stats.SummonCost)
        {
            stateMachine.ChangeState(stateMachine.MeleeState);
        }
    }
}
