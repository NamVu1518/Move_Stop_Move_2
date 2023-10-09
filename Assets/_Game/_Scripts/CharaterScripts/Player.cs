using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Charater
{
    [Header("Object")]
    [SerializeField] private Transform scopeTrans;

    [Header("Other")]
    public Vector3 initPoint;

    private float clock;

    protected void Start()
    {
        initPoint = tf.position;
        SetSkinsMaterial();
    }

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            Move();
            Attack();
            ScopeControl();
        }
        else
        {
            ChangeAnim(Constant.ANI_IDLE);
        }
    }


    #region LOGIC
    public override void Move()
    {
        if (isHited == false)
        {
            if (Joystick.GetDirect().sqrMagnitude >= 0.1f)
            {
                ChangeAnim(Constant.ANI_RUN);

                tf.rotation = Quaternion.LookRotation(Joystick.GetDirect().normalized);
                isAcceptAttack = true;
                rb.velocity = Joystick.GetDirect() * speed;
                clock = Constant.TIME_DELAY_THROW_BULLET;
            }
            else
            {
                ChangeAnim(Constant.ANI_IDLE);

                rb.velocity = Vector3.zero;
            }
        }
    }

    public override void Attack()
    {
        if (isHited == false)
        {
            clock -= Time.deltaTime;

            if (rb.velocity.sqrMagnitude < 0.1f && listCharacterTarget.Count > 0 && isAcceptAttack == true)
            {
                ChangeAnim(Constant.ANI_ATTACK);

                Vector3 direct = new Vector3(listCharacterTarget[0].tf.position.x, this.tf.position.y, listCharacterTarget[0].tf.position.z);
                tf.LookAt(direct, Vector3.up);

                if (clock <= 0f)
                {
                    DisappearWeaponWhenFire();
                    weapon.Fire(this, materialsBullet);
                    isAcceptAttack = false;
                }
            }
        }
    }

    private void ScopeControl()
    {
        if (isHited == false)
        {
            if (listCharacterTarget.Count > 0)
            {
                scopeTrans.gameObject.SetActive(true);

                scopeTrans.position = new Vector3(listCharacterTarget[0].tf.position.x, tf.position.y, listCharacterTarget[0].tf.position.z);
            }
            else if (listCharacterTarget.Count <= 0)
            {
                scopeTrans.gameObject.SetActive(false);
            }
        }
        else
        {
            scopeTrans.gameObject.SetActive(false);
        }
    }

    protected override void SelectWeapon(EnumBulletAndWeaponssData enumBulletsData)
    {
        base.SelectWeapon(enumBulletsData);

        weapon.ChangeMaterial(DataManager.Ins.GetMaterialArrayEquippedInThisWeapon((int)enumBulletsData - 1));
        materialsBullet = DataManager.Ins.GetMaterialArrayEquippedInThisWeapon((int)enumBulletsData - 1);
    }

    public void SetSkinsMaterial()
    {
        skin.material = DataManager.Ins.GetMaterialEquippedInThisSlot(EnumSkinType.skinSlot);
        pant.material = DataManager.Ins.GetMaterialEquippedInThisSlot(EnumSkinType.pantSlot);
    }
    #endregion


    #region LIFE CYCLE
    public override void OnInit()
    {
        base.OnInit();

        tf.position = initPoint;
        clock = Constant.TIME_DELAY_THROW_BULLET;
        SelectWeapon(DataManager.Ins.GetEnumWeaponEquipped());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        GameManager.Ins.Lose();
    }
    #endregion
}


public class DataPlayer
{
    private string name;
    private int coin;
    private int level;

    public string Name
    {
        set { name = value; }
        get { return name; }
    }

    public int Coin
    {
        set { coin = value; }
        get { return coin; }
    }

    public int Level
    {
        set { level = value; }
        get { return level; }
    }



    public DataPlayer LoadData()
    {
        DataPlayer dataPlayer = new DataPlayer();

        dataPlayer.name = PlayerPrefs.GetString(Constant.KEY_NAME, "Player_1");
        dataPlayer.coin = PlayerPrefs.GetInt(Constant.KEY_COIN, 0);
        dataPlayer.level = PlayerPrefs.GetInt(Constant.KEY_LEVEL, 1);

        return dataPlayer;
    }

    public void SaveData(DataPlayer dataPlayer)
    {
        PlayerPrefs.SetString(Constant.KEY_NAME, dataPlayer.name);
        PlayerPrefs.SetInt(Constant.KEY_COIN, dataPlayer.coin);
        PlayerPrefs.SetInt(Constant.KEY_LEVEL, dataPlayer.level);
    }
}
