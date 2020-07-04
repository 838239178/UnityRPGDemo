using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    public void AddUnit(GameObject unit)
    {
        unit.transform.parent = this.transform;
        //TO DO: 计算新单位在战斗画面的位置 根据PositionNo
    } 
}
