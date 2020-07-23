using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* rpg任务系统 基本要求
 * 多个任务同时进行
 * 每个任务有不同的结果（奖励/惩罚/剧情走向）
 * 包含多种形式（打败敌人/收集材料/与npc对话、传递任务物品）
 */


public class TaskSystem : MonoBehaviour
{
    public const int CANCEL = -6868;
    public const int FINISH = -9008;
    public const int ACHIEVE = 0;

    public static TaskSystem current;

    public SpawnEnemy spawner;

    [Header("执行中的任务ID")] [SerializeField]
    private List<string> tasks;

    private void Start()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake() 
    {
        if(current == null) 
        { 
            //TODO: 存档中读取以接受的任务
        }
       
    }
    private void OnDestroy()
    {
        //TODO: 保存已接受的任务ID
    }

    public void AddTask(string taskID)
    {
        if (tasks.Contains(taskID)) return;

        tasks.Add(taskID);
        //如果是杀敌任务
        BattleTrigger thisTrigger = spawner.SpawnEnemyByTask(taskID, delegate { FinishTask(taskID); });
    }

    public void FinishTask(string taskID) 
    {
        tasks.Remove(taskID);
        PlayerPrefs.SetInt(taskID, FINISH);
        //TODO: 给予任务奖励

        Debug.Log(taskID + " task finished!");
    }

    public void CancelTask(string taskID)
    {
        tasks.Remove(taskID);
        PlayerPrefs.SetInt(taskID, CANCEL);

        Debug.Log(taskID + " task cancel!");
    }
}
