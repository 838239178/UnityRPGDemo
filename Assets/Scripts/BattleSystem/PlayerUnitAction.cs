using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionType
{
    PhysicalAttack,
    MagicalAttack,
    MagicalSelect,
    Escape,
    Idle,
}
public class PlayerUnitAction : MonoBehaviour
{
    [HideInInspector]
    public ActionType actionType;
    [HideInInspector]
    public SkillCell useSkill;

    private AttackTarget attacker;
    
    private void OnEnable()
    {
        attacker = GameObject.Find("AttackManager")?.GetComponent<AttackTarget>();
    }
    
    private void PhysicalAttack(GameObject target)
    {
        attacker.Hit(target);
    }

    private void MagicalAttack(GameObject target)
    {
        //TO DO: 根据技能名从技能列表中提取相关信息并提供给attacker
        attacker.isMagic = true;
        attacker.MPCost = float.Parse(useSkill.Data.cost);
        if(!attacker.Hit(target, useSkill))
        {
            Debug.Log(this.name + "'s MP is not enough");
        }
    }

    private void Escape(GameObject target)
    {
        attacker.Escape(target);
    }

    public void Act(GameObject target)
    {
        attacker.owner = this.gameObject;
        attacker.isMagic = (this.actionType == ActionType.MagicalAttack);
        switch (actionType)
        {
            case ActionType.PhysicalAttack:
                this.PhysicalAttack(target);
                break;
            case ActionType.MagicalAttack:
                this.MagicalAttack(target);
                break;
            case ActionType.Escape:
                this.Escape(target);
                break;
        }
    }

}
