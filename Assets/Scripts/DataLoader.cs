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
        if (dataAsset.skillData == null)
            Debug.LogError("Excle load failed");
    }
}
