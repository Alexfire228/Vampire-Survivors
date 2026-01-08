using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private Animator animator;

    private EnemyStats enemyStats;

    public AttackState(NecromancerStateMachine sm, EnemyStats stats)
    {
        stateMachine = sm;
        enemyStats = stats;
        animator = stateMachine.GetComponent<Animator>();
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
        
    }
}
