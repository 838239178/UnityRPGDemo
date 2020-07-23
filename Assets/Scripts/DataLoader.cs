using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    private void Awake()
    {
        GameData dataAsset = Resources.Load<GameData>("GameData");
        if (dataAsset == null)
            Debug.LogError("GameData load failed");
        dataAsset.skillData = ExcelReader<Skill>.ReadDataExcel("Assets/excle/skill.xlsx");
        dataAsset.equipData = ExcelReader<ItemEquip>.ReadDataExcel("Assets/excle/equip.xlsx");
        if (dataAsset.skillData == null)
            Debug.LogError("skill data load failed");
        if (dataAsset.equipData == null)
            Debug.LogError("equip data load failed");
    }
}
