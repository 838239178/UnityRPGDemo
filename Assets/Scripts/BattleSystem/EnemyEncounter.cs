using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    public bool actionable = false;
    public uint memberCount = 0;

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //每次切换场景计算队伍信息
        memberCount = (uint)this.transform.childCount;
        actionable = false;
        if (memberCount > 0) actionable = true;
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        Debug.Log(this.name + " scene's loading event add successs");
    }

    public void AddUnit(GameObject unit)
    {
        unit.transform.parent = this.transform;
        //TO DO: 计算新单位在战斗画面的位置 根据PositionNo
    } 
}
