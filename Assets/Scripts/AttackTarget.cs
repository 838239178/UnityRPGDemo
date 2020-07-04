﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    [HideInInspector]
    public GameObject owner;
    [HideInInspector]
    public bool isMagic;
    [HideInInspector]
    public string atkAnimName;
    [HideInInspector]
    public float MPCost;

    [SerializeField] private GameObject messageBox;
    [SerializeField] private float maxAtkMultiplier;
    [SerializeField] private float minAtkMultiplier;
    [Range(1,10)]
    [SerializeField] private float escapeChance;

    private TurnSystem turnSys;

    private void Start()
    {
        turnSys = GameObject.Find("TurnSystem").GetComponent<TurnSystem>();
    }

    public bool Hit(GameObject target, SkillCell skill = null)
    {
        atkAnimName = this.owner.name + "_atk";
        UnitStats ownerStats = owner.GetComponent<UnitStats>();
        UnitStats targetStats = target.GetComponent<UnitStats>();
        if (!isMagic) MPCost = 0;
        if(ownerStats.realMP >= MPCost)
        {
            float currentAtkMultipilier = (Random.value * (maxAtkMultiplier - minAtkMultiplier)) + minAtkMultiplier;
            float damage = currentAtkMultipilier * (isMagic ? ownerStats.INT+float.Parse(skill.Data.damage) : ownerStats.ATK);
            damage = Mathf.Max(0, damage - targetStats.DEF * .25f);
            //播放伤害信息
            messageBox.SetActive(true);
            messageBox.GetComponent<MessageManager>().SetAtkText(owner.name, target.name, damage,isMagic);
            //播放攻击动画  
            ownerStats.anim.Play(atkAnimName);
            if (isMagic)
            {
                skill.audio.Play();
                Debug.Log("skill sound played");
            }
            StartCoroutine(ReceiveDamageWait(targetStats, damage));
            ownerStats.realMP -= MPCost;
            StartCoroutine("TurnWait");
            return true;
        }
        return false;
    }

    public void Escape(GameObject target)
    {
        float f = Random.Range(1f, 10f);
        float additionalChance = owner.GetComponent<UnitStats>().SPD / target.GetComponent<UnitStats>().SPD;

        if (f < escapeChance*additionalChance)
            turnSys.SetEscape();
        else
        {
            messageBox.SetActive(true);
            messageBox.GetComponent<MessageManager>().SetText("逃跑失败");
            StartCoroutine("TurnWait");
        }
    }

    IEnumerator TurnWait()
    {
        yield return new WaitForSeconds(2f);
        //关闭伤害信息播放
        messageBox.SetActive(false);
        turnSys.NextTurn();
        Debug.Log("TurnWait over. time passed 1 second");
    }
    IEnumerator ReceiveDamageWait(UnitStats targetStats, float damage)
    {
        yield return new WaitForSeconds(0.5f);
        targetStats.ReceiveDamage(damage);
        Debug.Log("ReceiveDamageWait over. time passed 1 second");
    }
}
