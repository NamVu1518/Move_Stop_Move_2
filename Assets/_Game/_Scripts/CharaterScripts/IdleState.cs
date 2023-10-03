using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IStateEnemy<Enemy>
{
    public void OnInit(Enemy t)
    {
        
    }

    public void OnExcute(Enemy t)
    {
        t.ChangeAnim("IsIdle");
    }

    public void OnOut(Enemy t)
    {
        throw new System.NotImplementedException();
    }
}
