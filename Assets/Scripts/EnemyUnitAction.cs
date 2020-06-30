using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitAction : MonoBehaviour
{
    enum Type
    { 
        Top,
        MaxDEF, 
        MaxHP, 
        MinHP, 
        MinDEF,
    }

    [SerializeField]
    private string targetsTag;
    [SerializeField]
    private Type priorityTargetType;

    private AttackTarget attacker;

    private void OnEnable()
    {
        attacker = GameObject.Find("AttackManager")?.GetComponent<AttackTarget>();
    }
    private  GameObject FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if(targets.Length > 0)
        {
            GameObject res = null;
            switch (priorityTargetType)
            {
                case Type.Top:
                    res = targets[0];
                    break;
            }
            return res;
        }
        return null;
    }
    public void Act()
    {
        attacker.owner = this.gameObject;
        GameObject target = FindTarget();
        //使用物理攻击
        attacker.isMagic = false;
        attacker.MPCost = 0;
        attacker.atkAnimName = this.gameObject.name + "_atk";
        attacker.Hit(target);
    }
}
