using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBase : GameUnit
{
    [Header("Enum Weapon Data")]
    public EnumBulletAndWeaponssData enumWeaponsData;

    [Header("Transform Weapon Child")]
    [SerializeField] protected Transform bulletChild;
    [SerializeField] protected GameObject bulletRaw;

    internal Charater charater;

    protected Transform charaterTrans;

    protected BulletData weaponData;

    protected float clock;

    public virtual void OnInit(Charater charater, EnumBulletAndWeaponssData enumWeaponsData, Material[] materials)
    {
        ChangeMaterialBullet(materials);
        this.charater = charater;
        this.charaterTrans = this.charater.transform;
        this.clock = 0;
        this.weaponData = DataManager.Ins.TakeWeaponData(enumWeaponsData);
        this.tf.localScale += new Vector3(1, 1, 1) * charater.LevelPlayer * Constant.NUM_SCALE_CHARACTER;
    }

    

    protected virtual void OnExcute() { }

    protected virtual void OnReturn() { }

    protected virtual void OnDespawn() { }

    protected void RunFlowStraightOnly(GameUnit gameUnit)
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            this.clock += Time.deltaTime;

            this.BulletDespawnIfCharaterDead(gameUnit);

            this.OnExcute();

            if (this.clock >= this.weaponData.existOrReturnTime)
            {
                this.OnDespawn();
            }
        }
    }

    protected void RunFlowReturn(GameUnit gameUnit)
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            this.clock += Time.deltaTime;

            this.BulletDespawnIfCharaterDead(gameUnit);

            if (this.clock < this.weaponData.existOrReturnTime)
            {
                this.OnExcute();
            }
            else if (this.clock >= this.weaponData.existOrReturnTime)
            {
                this.OnReturn();
            }
        }
    }

    protected void BulletStraight(Transform weaponTransform, Vector3 vtDirect, float speed)
    {
        weaponTransform.position += vtDirect * speed;
    }


    protected void BulletRotation(Transform weaponChildTransform)
    {
        weaponChildTransform.Rotate(Vector3.up, Constant.SPEED_ROTATE_WEAPON_VT_Y * Time.deltaTime);
    }

    protected void BulletSwirl(Transform weaponChildTransform)
    {
        weaponChildTransform.Rotate(Vector3.forward, Constant.SPEED_ROTATE_WEAPON_VT_Z * Time.deltaTime);
    }

    protected void BulletReturn(Transform weaponTransform, Vector3 vtTarget, float speed)
    {
        if (Vector3.Distance(vtTarget, weaponTransform.position) <= 1f)
        {
            this.OnDespawn();
        }

        Vector3 vtDirect = vtTarget - weaponTransform.position;
        weaponTransform.position += vtDirect.normalized * speed;
    }

    protected void BulletDespawn(GameUnit gameUnit)
    {
        this.charater = null;
        this.clock = 0;
        this.tf.localScale = new Vector3(1, 1, 1);
        PoolSimple.Despawn(gameUnit);
    }

    protected void BulletDespawnIfCharaterDead(GameUnit gameUnit)
    {
        if(this.charater.IsHited == true)
        {
            this.BulletDespawn(gameUnit);
        }
    }

    protected void BulletDamage(Collider collider)
    {
        if (collider.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            Charater charater = Cache.GetCharaterComponentForCollider(collider);

            if (charater != this.charater)
            {
                charater.OnHit();
                this.charater.CountKill();
                this.OnDespawn();
            }
        }
    }

    protected void ChangeMaterialBullet(Material[] materials)
    {
        Cache.GetRendererComponentGameObject(this.bulletRaw).materials = materials;
    }
}
