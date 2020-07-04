using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实例化预制体，创建技能菜单
/// </summary>
public class SkillLayer : MonoBehaviour
{
    [SerializeField]
    private GameObject CellPerfab;
    [SerializeField]
    private GameData asset;
    public GameData GameDataAsset { get => asset; } 

    private  UnitStats preUnit;

    public void CreateSkillMenu(UnitStats unit)
    {
        if(preUnit == null)
        {
            preUnit = unit;
            NewInstantiate(unit);
        }
        else if (preUnit != unit)
        {
            //充分利用前一轮生成过的资源
            for (int childIndex = 0, i = 0; i<unit.skill.Length && childIndex<this.transform.childCount; childIndex++, i++)
            {
                GameObject cell = this.transform.GetChild(childIndex).gameObject;
                if(!cell.activeSelf) cell.SetActive(true);
                cell.GetComponent<SkillCell>().Data = asset.skillData[unit.skill[i]];
            }
            if(this.transform.childCount > unit.skill.Length)
            {
                //如果新unit的技能比较少 隐藏后面几个cell
                for(int i = unit.skill.Length; i< this.transform.childCount; i++)
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            else if (this.transform.childCount < unit.skill.Length)
            {
                //如果新unit的技能比较多 新建多出来的
                NewInstantiate(unit, this.transform.childCount);
            }
        }

    }

    private void NewInstantiate(UnitStats unit, int begin = 0)
    {
        for(int i = begin; i<unit.skill.Length; i++)
        {
            GameObject cell = GameObject.Instantiate(CellPerfab, this.transform);
            cell.GetComponent<SkillCell>().Data = asset.skillData[unit.skill[i]];
        }
    }
}
