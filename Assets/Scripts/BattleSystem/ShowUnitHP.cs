using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUnitHP : ShowUnitStat
{
     override protected float newStatValue()
    { 
        //throw new System.NotImplementedException();
        return unitStat.realHP;
    }
    override public void SetMaxValue(float date)
    {
        this.maxValue = unitStat.HP;
    }
}
