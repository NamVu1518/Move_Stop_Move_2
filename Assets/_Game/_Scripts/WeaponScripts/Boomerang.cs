using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boomerang : BulletBase
{

    void Update()
    {
        RunFlowReturn(this);
    }

    protected override void OnExcute()
    {
        BulletStraight(tf, tf.forward, weaponData.speed);
        BulletRotation(bulletChild);
    }

    protected override void OnReturn()
    {
        BulletReturn(tf, charater.tf.position + Constant.VECTOR_SPAWN_WEAPON, weaponData.speed);
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
