using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IStateEnemy<Enemy>
{
    private float clock;

    public void OnInit(Enemy t)
    {
        clock = 0;
    }

    public void OnExcute(Enemy t)
    {
        clock += Time.deltaTime;

        t.ChangeAnim(Constant.ANI_RUN);

        t.Move();

        if (clock >= 1.5f && t.listCharacterTarget.Count > 0) 
        {
            t.ChangeState(new AttackState());
        }
    }

    public void OnOut(Enemy t)
    {
        
    }
}
