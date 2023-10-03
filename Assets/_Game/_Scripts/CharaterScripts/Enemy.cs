using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Enemy : Charater
{
    [Header("Agent")]
    public NavMeshAgent agent;

    private IStateEnemy<Enemy> currentState;

    private Vector3 vtDestination;
    public bool IsDestionation => Vector3.Distance(tf.position, vtDestination + (tf.position.y - vtDestination.y) * Vector3.up) < 5f;

    private bool isInit = false;

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (!isInit)
            {
                return;
            }

            if (isHited == false)
            {
                currentState?.OnExcute(this);
            }
        }
        else
        {
            ChangeAnim("IsIdle");

            agent.SetDestination(tf.position);
        }
    }








    #region Logic
    public override void Move()
    {
        if (IsDestionation)
        {
            FindDestination();
        }
        else
        {
            SetDestination(vtDestination);
        }
    }

    public override void Attack()
    {
        Vector3 direct = new Vector3(listCharacterTarget[0].tf.position.x, this.tf.position.y, listCharacterTarget[0].tf.position.z);
        tf.LookAt(direct, Vector3.up);

        weapon.Fire(this, materialsBullet);

        isAcceptAttack = false;
    }

    private void FindDestination()
    {
        Charater charater = CharacterManage.Ins.listActive[UnityEngine.Random.Range(0, CharacterManage.Ins.listActive.Count)];
        vtDestination = charater.tf.position;
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void ChangeState(IStateEnemy<Enemy> newState)
    {
        if (currentState != null)
        {
            currentState.OnOut(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnInit(this);
        }
    }
    #endregion


    #region Life Circle
    public override void OnInit()
    {
        base.OnInit();

        
        SelectWeapon(DataManager.Ins.GetEnumWeapon(UnityEngine.Random.Range(1, DataManager.Ins.EnumBulletsAndWeaponsDataLenght())));
        agent.speed = speed;
        FindDestination();
        isInit = true;
        ChangeState(new PatrolState());
    }

    protected override void SelectWeapon(EnumBulletAndWeaponssData enumBulletsData)
    {
        base.SelectWeapon(enumBulletsData);

        int numSlotsArray = DataManager.Ins.SlotsAmount((int)weapon.bulletPrefab.enumWeaponsData - 1);

        materialsBullet = new Material[numSlotsArray];

        for (int i = 0; i < numSlotsArray; i++)
        {
            int rd = UnityEngine.Random.Range(0, DataManager.Ins.EnumMaterialsWeaponLenght());
            EnumMaterialsWeaponData enumMaterialsColorData = (EnumMaterialsWeaponData)Enum.Parse(typeof(EnumMaterialsWeaponData), rd.ToString());
            materialsBullet[i] = DataManager.Ins.GetMaterial(enumMaterialsColorData);
        }

        weapon.ChangeMaterial(materialsBullet);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        isInit = false;
        CharacterManage.Ins.RemoveCharaterFromListActive(this);
    }
    #endregion
}