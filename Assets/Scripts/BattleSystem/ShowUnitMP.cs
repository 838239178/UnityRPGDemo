using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitMP : ShowUnitStat
{
    override protected float newStatValue()
    {
        //throw new System.NotImplementedException();
        return unitStat.realMP;
    }
    public override void SetMaxValue(float date)
    {
        //throw new System.NotImplementedException();
        this.maxValue = unitStat.MP;
    }
}
