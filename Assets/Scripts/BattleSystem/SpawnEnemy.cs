using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
///  require NPC or System Cycle placement to place enemy. 
/// </summary>
public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy current = null;

    public GameObject enemyEncounter;
    public GameObject playerParty;

    [SerializeField]
    private GameObject battleTrigger; 
    [SerializeField]
    private GameObject enemyPerfabs;

    private void Awake()
    {
        enemyEncounter = GameObject.Find("EnemyEncounter");
        playerParty = GameObject.Find("PlayerParty");
    }

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

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle") this.gameObject.SetActive(false);
        else this.gameObject.SetActive(true);
    }

    public BattleTrigger SpawnEnemyByTask(string taskID, UnityAction myAction)   
    {
        //TO DO: 实例化一个触发器作为子对象 将触发器的位置设置成指定位置 
        //       触发器自行生成自己对应的敌人队伍 通过设定好的数据
        BattleTrigger newTrigger = this.transform.GetChild(0).gameObject.GetComponent<BattleTrigger>();
        newTrigger.relativeTask = taskID;
        newTrigger.gameObject.SetActive(true);
        newTrigger.OnBattleWin.AddListener(myAction);

        return newTrigger;
    }

    
}
