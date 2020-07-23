using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IComparable
{
    public Animator anim; 
    public Sprite unitFace;
    public int NextActTurn;
    public int PositionNo;                  //战斗时的排位顺序 ： 0-5前卫 6-10中卫 11-15后卫
    
    [HideInInspector]
    public float realHP;
    [HideInInspector]
    public float realMP;

    [Header("单位的基本属性")]
    [Range(0,999)]
    public float HP;
    [Range(0, 999)]
    public float MP;
    [Range(0, 256)]
    public float ATK; 
    [Range(0, 256)]
    public float INT;
    [Range(0, 256)]
    public float DEF;
    [Range(0, 256)]
    public float SPD;

    [Header("单位拥有的技能ID")]
    public uint[] skill;

    private bool dead = false;

    private void Awake()
    {
        //TO DO: 从存档中读取

        realHP = HP;
        realMP = MP;
    }

    private void OnDestroy()
    {
        //TODO: 保存属性
    }

    public void ReceiveDamage(float damage)
    {
        realHP = Mathf.Max(0, realHP - damage);
        anim.Play(this.gameObject.name + "_hit");

        //TO DO: 在信息栏播放战斗信息
        Debug.Log(this.name + " have recived " + damage);

        if (realHP == 0)
        {
            if (this.gameObject.tag == "EnemyUnit") Destroy(this.gameObject);
            dead = true;
            this.gameObject.tag = "DeadUnit";
            this.gameObject.SetActive(false);
            Debug.Log(this.name + " is dead");
        }
    }
    public void CalculateNextActTurn(int currentTurn)
    {
        NextActTurn = currentTurn + (int)Math.Ceiling(100.0f / SPD);
    }
    public void CalculateNextActTurn()
    {
        NextActTurn = NextActTurn + (int)Math.Ceiling(100.0f / SPD);
    }
    public int CompareTo(object other)
    {
        return NextActTurn.CompareTo(((UnitStats)other).NextActTurn);
    }
    public bool isDead()
    {
        return dead;
    }
}
