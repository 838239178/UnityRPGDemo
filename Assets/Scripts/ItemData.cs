using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;

[Serializable]
public struct ItemIncreaseValue
{
    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public int INT;
    public int SPD;
}

public enum EquipType
{
    Head,
    Body,
    Weapon,
    Shield,
}

public enum EquipQuality
{
    Tattered,
    Fine,
    Epic,
}

public abstract class ItemBase : IDataInitialization
{
    public string ID;
    public string itemName;
    public string iconAtlas; 
    public string icon;
    public string value;
    public int itemHeiled = 1;
    public ItemIncreaseValue effect = new ItemIncreaseValue();
    [TextArea]
    public string describe;

    public abstract void Initial(DataRow collect);
}

[Serializable]
public class ItemEquip : ItemBase
{
    public EquipType type;
    public EquipQuality quality = EquipQuality.Fine;
    public int levelRequire = 1;

    //0-ID 1-Name 2-value 3-hp 4-mp 5-atk 6-def 7-int 8-spd 9-type 10-describe 11-iconaltas 12-icon
    public override void Initial(DataRow collect)
    {
        ID = collect[0].ToString();
        itemName = collect[1].ToString();
        value = collect[2].ToString();
        effect.HP = int.Parse(collect[3].ToString());
        effect.MP = int.Parse(collect[4].ToString());
        effect.ATK = int.Parse(collect[5].ToString());
        effect.DEF = int.Parse(collect[6].ToString()); 
        effect.INT = int.Parse(collect[7].ToString());
        effect.SPD = int.Parse(collect[8].ToString());

        foreach(EquipType eType in Enum.GetValues(typeof(EquipType)))
        {
            if(eType.ToString() == collect[9].ToString())
            {
                this.type = eType;
                break;
            }
        }

        describe = collect[10].ToString();
        iconAtlas = collect[11].ToString();
        icon = collect[12].ToString();
    }
}