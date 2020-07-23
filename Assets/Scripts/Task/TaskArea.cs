using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 负责触发任务，通知任务系统开始一个任务
/// </summary>
public class TaskArea : MonoBehaviour 
{
    public bool directlyAchieve = false; 
    public string taskID;
    public bool accomplished = false;

    [SerializeField]
    private bool invalid = true;            //任务在未接受前以及完成后都是失效的

    private void Update()
    {
        if (!invalid)
        {
            if (PlayerPrefs.GetInt(taskID) == TaskSystem.FINISH)
            {
                accomplished = true;
                invalid = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (directlyAchieve)
        {
            TaskSystem.current.AddTask(taskID);
            Debug.Log(taskID + " task begin!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !directlyAchieve && !accomplished)
        {
            if(PlayerPrefs.GetInt(taskID,-1) == TaskSystem.ACHIEVE)
            {
                //如果已经添加过任务 invalid 会设置为 false 所以不需要再次添加
                if (!invalid) return;
                invalid = false;
                TaskSystem.current.AddTask(taskID);
                Debug.Log(taskID + " task begin!");
            }
        }

    }
}   
