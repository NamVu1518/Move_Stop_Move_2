using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BulletBase
{

    void Update()
    {
        RunFlowStraightOnly(this);
    }

    protected override void OnExcute()
    {
        BulletStraight(tf, tf.forward, weaponData.speed);
        BulletSwirl(bulletChild);
    }

    protected override void OnDespawn()
    {
        BulletDespawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        BulletDamage(other);
    }
}
