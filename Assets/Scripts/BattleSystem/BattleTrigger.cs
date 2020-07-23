using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour
{
    //在实例化时，从spawnEnemy中获取
    public GameObject enemyEncounter;
    public GameObject playerParty;

    public ChangeScene levelLoader;

    public UnityEvent OnBattleWin;

    public string relativeTask = "";

    private void Start()
    {
        enemyEncounter = SpawnEnemy.current.enemyEncounter;
        playerParty = SpawnEnemy.current.playerParty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyEncounter.GetComponent<EnemyEncounter>().actionable && collision.tag == "Player")
        {
            GameData gmData = Resources.Load<GameData>("GameData");

            if(relativeTask != "")
            {
                //TODO: 在enemyencounter生成敌人
                Debug.Log("enemy Initialization");

            }
            if (playerParty.GetComponent<PlayerStat>().actionable == false) return;
            levelLoader.LoadNextScene("Battle");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!enemyEncounter.GetComponent<EnemyEncounter>().actionable)
        {
            OnBattleWin.Invoke();
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
