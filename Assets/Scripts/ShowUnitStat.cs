using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ShowUnitStat : MonoBehaviour
{
    [SerializeField]
    protected UnitStats unitStat;

    [SerializeField]
    protected float maxValue;

    private Vector2 initialScale;

    abstract protected float newStatValue();
    abstract public void SetMaxValue(float date);

    private void Start()
    {
        initialScale = this.gameObject.transform.localScale;
    }

    void Update()
    {
        if (this.unitStat)
        {
            float newValue = this.newStatValue();
            float newScale = (this.initialScale.x * newValue) / this.maxValue;
            this.gameObject.transform.localScale = new Vector2 (newScale,this.initialScale.y);
        }
    }

    public void SetUnit(GameObject unit)
    { 
        this.unitStat = unit.GetComponent<UnitStats>();
    }
}