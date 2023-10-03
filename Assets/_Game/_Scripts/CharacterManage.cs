using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManage : Singleton<CharacterManage>
{
    internal int numAliveCharater = Constant.NUM_OF_CHARATER_IN_MAP;

    internal int numEnemySpawned = 0;

    public List<Charater> listActive = new List<Charater>();

    [SerializeField] private Player player;
    public Player Player
    {
        get { return player; }
    }


    public void OnInit()
    {
        listActive.Clear();
        player.OnInit();

        numAliveCharater = Constant.NUM_OF_CHARATER_IN_MAP;
        numEnemySpawned = 0;

        for (int i = 0; i < Constant.NUM_OF_ENEMY_IN_MAP; i++)
        {
            SpawnNewEnemy();
        }
    }

    public void AddCharaterToListActive(Charater charater)
    {
        listActive.Add(charater);
    }

    public void RemoveCharaterFromListActive(Charater charater)
    {
        PoolSimple.Despawn(charater);
        listActive.Remove(charater);
        numAliveCharater--;
        
        if (listActive.Count == 1 && listActive[0] is Player) 
        {
            GameManager.Ins.Win();
        }

        SpawnNewEnemy();
    }

    public void SpawnNewEnemy()
    {
        if (numEnemySpawned < Constant.NUM_OF_ENEMY_SPAWN)
        {
            if (NavMesh.SamplePosition(RandomSpawnPosition(), out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                Charater charater = PoolSimple.Spawn(PoolType.enemy, hit.position, Quaternion.identity) as Charater;
                charater.OnInit();
                numEnemySpawned++;
            }
        } 
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-50, 51), 0.58f, Random.Range(-50, 51));
    }
}
