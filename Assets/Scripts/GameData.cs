using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using UnityEngine;
using UnityEditor;


[Serializable]
public class Skill : IDataInitialization
{
    public string ID;
    public string iconAtlas;
    public string icon;
    public string sound;
    public string skillName; 
    public string damage;
    public string cost;
    [TextArea]
    public string describe;
    //行： 0-ID, 1-NAME, 2-COST, 3-DAMAGE, 4-DESCRIBE, 5-ICON Atlas, 6-ICON, 7-SOUND
    public void Initial(DataRow collect)
    {
        ID = collect[0].ToString();
        skillName = collect[1].ToString();
        cost = collect[2].ToString();
        damage = collect[3].ToString();
        describe = collect[4].ToString();
        iconAtlas = "Icons/" + collect[5].ToString();
        icon = collect[6].ToString();
        sound = "music/" + collect[7].ToString();
    }
}

[CreateAssetMenu(fileName = "creat", menuName = "GameData")]
public class GameData : ScriptableObject
{
    public Skill[] skillData;
    public ItemEquip[] equipData;
}
