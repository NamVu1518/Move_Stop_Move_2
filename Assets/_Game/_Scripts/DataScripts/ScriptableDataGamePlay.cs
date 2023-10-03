using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public enum EnumBulletAndWeaponssData
{
    none = 0,
    arrow = 1,
    axe_0 = 2,
    axe_1 = 3,
    boomerang = 4,
    candy_0 = 5,
    candy_1 = 6,
    candy_2 = 7,
    candy_4 = 8,
    hammer = 9,
    knife = 10,
    uzi = 11,
    z = 12,
}

[CreateAssetMenu(menuName = "DataGamePlay", fileName = "DataGamePlay") ]
public class ScriptableDataGamePlay : ScriptableObject
{
    public List<BulletData> bulletsOriginalData = new List<BulletData>();

    public List<Weapon> listWeapon = new List<Weapon>();


    public BulletData TakeBulletsOriginalData(EnumBulletAndWeaponssData weaponOriginal)
    {
        return bulletsOriginalData[(int)weaponOriginal];
    }

    public Weapon GetWeapon(EnumBulletAndWeaponssData enumWeapons)
    {
        return listWeapon[(int)enumWeapons];
    } 
}




[System.Serializable]
public class BulletData
{
    public string nameWeapon;
    public float existOrReturnTime;
    public float speed;
}



