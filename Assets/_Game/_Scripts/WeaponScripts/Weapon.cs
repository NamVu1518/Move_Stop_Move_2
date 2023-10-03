using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BulletBase bulletPrefab;
    public GameObject goWeapon;
    public GameObject goWeaponRaw;

    private Transform charaterTrans;

    public void Fire(Charater charater, Material[] materials)
    {
        charaterTrans = charater.transform;

        BulletBase bulletBase = Spawn(bulletPrefab.poolType, charaterTrans);
        bulletBase.OnInit(charater, bulletPrefab.enumWeaponsData, materials);
    }

    public BulletBase Spawn(PoolType poolType, Transform charaterTrans)
    {
        return PoolSimple.Spawn(poolType, charaterTrans.position + Constant.VECTOR_SPAWN_WEAPON, charaterTrans.rotation) as BulletBase;
    }

    public void ChangeMaterial(Material[] materials)
    {
        Cache.GetRendererComponentGameObject(goWeaponRaw).materials = materials;
    } 
}
