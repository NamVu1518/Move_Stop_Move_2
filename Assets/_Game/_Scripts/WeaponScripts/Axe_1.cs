using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_1 : BulletBase
{
    void Update()
    {
        RunFlowStraightOnly(this);
    }

    protected override void OnExcute()
    {
        BulletStraight(tf, tf.forward, weaponData.speed);
        BulletRotation(bulletChild);
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
