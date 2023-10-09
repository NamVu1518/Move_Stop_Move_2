using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Charater : GameUnit
{
    public List<Charater> listCharacterTarget = new List<Charater>();

    [Header("Charater")]
    public float speed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Sight sight;
    [SerializeField] protected Renderer skin;
    [SerializeField] protected Renderer pant;

    [Header("Anim")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform theHand;

    [Header("Current Weapon")]
    [SerializeField] protected EnumBulletAndWeaponssData currentWeapon;

    protected int kill = 0;
    protected int killCountToLevelUp = 0;
    protected int levelPlayer = 0;
    
    public int Kill
    {
        get { return kill; }
    }

    public int LevelPlayer
    {
        get { return levelPlayer; }
    }

    protected bool isHited = false;
    public bool IsHited
    {
        get { return this.isHited; }
    }

    protected bool isAcceptAttack = true;

    protected string currentAnimName;

    internal Weapon weapon;
    protected Material[] materialsBullet;

    #region LOGIC
    public virtual void Move() { }

    public virtual void Attack() { }

    protected virtual void SelectWeapon(EnumBulletAndWeaponssData enumBulletsData) 
    {
        if (this.weapon != null) 
        {
            Destroy(this.weapon.gameObject);
        }

        this.weapon = Instantiate(DataManager.Ins.GetWeapon(enumBulletsData), this.theHand);
    }

    public void CountKill()
    {
        this.kill++;
        this.killCountToLevelUp++;

        ScaleLevelUp();
    }

    protected void ScaleLevelUp()
    {
        if (this.killCountToLevelUp >= Constant.NUM_KILL_CHARACTER_LEVEL_UP)
        {
            levelPlayer++;
            this.killCountToLevelUp = 0;
            this.tf.localScale += new Vector3(1, 1, 1) * Constant.NUM_SCALE_CHARACTER;
        }
    }
    #endregion

    #region LIFE CYCLE
    public virtual void OnInit()
    {
        this.tf = transform;
        this.tf.localScale = new Vector3(1, 1, 1);

        this.sight.gameObject.SetActive(true);
        this.isHited = false;
        
        this.kill = 0;
        this.killCountToLevelUp = 0;
        this.levelPlayer = 0;

        CharacterManage.Ins.AddCharaterToListActive(this);
    }

    public void OnHit() 
    {
        this.isHited = true;
        this.listCharacterTarget.Clear();
        this.sight.gameObject.SetActive(false);
        this.Dead();
    }

    protected void Dead() 
    {
        ChangeAnim(Constant.ANI_DEAD);
        this.StartCoroutine(IEDelay(this.OnDespawn, 1f));
    }

    public virtual void OnDespawn()
    {
        
    }

    public IEnumerator IEDelay(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action.Invoke();
    }
    #endregion

    #region ANIMATION
    public void ChangeAnim(string newAnimName)
    {
        if (newAnimName != this.currentAnimName)
        {
            this.animator.ResetTrigger(currentAnimName);
            this.currentAnimName = newAnimName;
            this.animator.SetTrigger(this.currentAnimName);
        }
    }

    protected void DisappearWeaponWhenFire()
    {
        this.weapon.gameObject.SetActive(false);
        StartCoroutine(nameof(AppearWeapon));
    } 

    private IEnumerator AppearWeapon()
    {
        yield return new WaitForSeconds(1f);
        this.weapon.gameObject.SetActive(true);
    } 
    #endregion
}
