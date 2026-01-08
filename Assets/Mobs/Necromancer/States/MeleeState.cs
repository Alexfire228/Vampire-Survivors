using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeState : BaseState
{
    private Animator animator;

    private EnemyStats enemyStats;

    public MeleeState(NecromancerStateMachine sm, EnemyStats stats)
    {
        stateMachine = sm;
        enemyStats = stats;
        animator = stateMachine.GetComponent<Animator>();
    }

    public override void Enter()
    {
        animator.SetBool("isMelee", true);
    }

    public override void Exit()
    {
        animator.SetBool("isMelee", false);
    }

    public override void LogicUpdate()
    {
        stateMachine.transform.position = Vector3.MoveTowards(stateMachine.transform.position, stateMachine.NearestPlayer.position, Time.deltaTime * enemyStats.Stats.Speed);

        if (enemyStats.Mana >= enemyStats.Stats.SummonCost)
        {
            stateMachine.ChangeState(stateMachine.SummonState);
        }

        if (stateMachine.NearestPlayer != null)
        {
            if (Vector3.Distance(stateMachine.transform.position, stateMachine.NearestPlayer.position) <= 2 && enemyStats.Mana < enemyStats.Stats.SummonCost)
            {
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }
    }
}
