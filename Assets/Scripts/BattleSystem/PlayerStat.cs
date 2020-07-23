using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public string userName;
    public uint money;
    public string sceneName;
    public Vector2 scenePosition;
    public uint memberCount = 0;
    public bool actionable = false;

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //每次切换场景计算队伍信息
        memberCount = (uint)this.transform.childCount;
        this.actionable = false;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject unit = this.transform.GetChild(i).gameObject;
            if(unit.GetComponent<UnitStats>().realHP > 0)
            {
                this.actionable = true;
                break;
            }
        }
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        Debug.Log(this.name + " scene's loading event add successs");

        //TO DO: 从存档读取信息
    }

    private void OnDestroy()
    {
        //TODO: 保存信息
    }
}
