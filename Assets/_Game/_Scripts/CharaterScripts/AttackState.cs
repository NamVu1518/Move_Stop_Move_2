using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IStateEnemy<Enemy>
{
    private float clock;

    public void OnInit(Enemy t)
    {
        clock = 0;
        t.SetDestination(t.tf.position);
        t.tf.LookAt(t.listCharacterTarget[0].tf.position + Vector3.up);
    }

    public void OnExcute(Enemy t)
    {
        clock += Time.deltaTime;
   
        if (clock >= 0.5f + Constant.TIME_DELAY_THROW_BULLET) 
        {
            if (t.listCharacterTarget.Count > 0)
            {
                t.Attack();
                t.ChangeState(new PatrolState());
            }
            else
            {
                t.ChangeState(new PatrolState());
            }
        }
        else if (clock >= 0.5f)
        {
            t.ChangeAnim(Constant.ANI_ATTACK);
        }
        else
        {
            t.ChangeAnim(Constant.ANI_IDLE);
        }
    }

    public void OnOut(Enemy t)
    {
        
    }
}
