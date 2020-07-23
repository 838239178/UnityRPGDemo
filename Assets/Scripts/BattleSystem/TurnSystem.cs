using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MyStructure;

public enum GameStatus
{
    EnemyAct,
    PlayerAct,
    Escaped,
    Win,
    Lose,
}
public class TurnSystem : MonoBehaviour
{

    public GameObject playerParty;
    public GameObject enemyEncounter;

    public MessageManager msgSender;

    [SerializeField]
    private GameObject overDialog;

    public GameObject SkillHit;

    private GameStatus nowStatus = GameStatus.PlayerAct;

    private PriorityQueue<UnitStats, Less<UnitStats>> unitsQueue = new PriorityQueue<UnitStats,Less<UnitStats>>();

    private GameObject nextUnit = null;

    public GameStatus GetStatusNow() { return nowStatus;  }

    public void SetEscape() { nowStatus = GameStatus.Escaped; }

    public void NextTurn()
    {
        if (OverStatus())  return;
        UnitStats stats = unitsQueue.Front;
        unitsQueue.Pop();
        if (!stats.isDead())
        {
            GameObject unit = stats.gameObject;
            SkillHit.transform.parent = unit.transform;
            stats.CalculateNextActTurn();
            unitsQueue.Push(stats);
            Debug.Log(unit.tag + " acting" + "\nNextTurn: " + stats.NextActTurn.ToString());
            if (unit.tag == "EnemyUnit")
            {
                nowStatus = GameStatus.EnemyAct;
                nextUnit = unit;
            }
            else if (unit.tag == "PlayerUnit")
            {
                nowStatus = GameStatus.PlayerAct;
                nextUnit = unit;
            }
                
        }
        else
        {
            NextTurn();
        }

    }

    private bool OverStatus()
    {
        bool winflag = true;
        bool loseflag = true;
        foreach (UnitStats ust in unitsQueue)
        {
            if (ust == null)
                continue;
            if (ust.tag == "PlayerUnit")
                loseflag = false;
            if (ust.tag == "EnemyUnit")
                winflag = false;
        }
        if (loseflag)
        {
            nowStatus = GameStatus.Lose;
            return true;
        }
        if (winflag)
        {
            nowStatus = GameStatus.Win;
            return true;
        }
        return false;
    }

    private void UpdateHUD(GameObject unit)
    {
        GameObject UnitFace = GameObject.Find("UnitFace");
        UnitFace.GetComponent<Image>().sprite = unit.GetComponent<UnitStats>().unitFace;

        GameObject HPBar = GameObject.Find("HPBar");
        HPBar.GetComponent<ShowUnitHP>().SetUnit(unit);
        HPBar.GetComponent<ShowUnitHP>().SetMaxValue(unit.GetComponent<UnitStats>().HP);

        GameObject MPBar = GameObject.Find("MPBar");
        MPBar.GetComponent<ShowUnitMP>().SetUnit(unit);
        MPBar.GetComponent<ShowUnitMP>().SetMaxValue(unit.GetComponent<UnitStats>().MP);
    }

    private void Update()
    {
        if(nowStatus == GameStatus.Win || nowStatus == GameStatus.Lose || nowStatus == GameStatus.Escaped)
        {
            overDialog.GetComponent<OverDialog>().onEnableStatus = nowStatus;
            overDialog.SetActive(true);
            Destroy(this.gameObject);
        }
        if (nextUnit)
        {
            if(nowStatus == GameStatus.PlayerAct)
            {
                this.playerParty.GetComponent<SelectUnit>().SetCurrentUnit(this.nextUnit);
                UpdateHUD(this.nextUnit);
            }
            else if(nowStatus == GameStatus.EnemyAct)
            {
                this.nextUnit.GetComponent<EnemyUnitAction>().Act();
            }
            nextUnit = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject unit in playerUnits)
        {
            UnitStats stats = unit.GetComponent<UnitStats>();
            stats.CalculateNextActTurn(0);
            unitsQueue.Push(stats);
        }
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject unit in enemyUnits)
        {
            UnitStats stats = unit.GetComponent<UnitStats>();
            stats.CalculateNextActTurn(0);
            unitsQueue.Push(stats);
        }

        playerParty = GameObject.Find("PlayerParty");
        enemyEncounter = GameObject.Find("EnemyEncounter");

        StartCoroutine(SendMsgWait("敌人拦在了你面前！", delegate { NextTurn(); }));     
    }

    IEnumerator SendMsgWait(string msg, UnityAction action)  
    {   
        yield return new WaitForSeconds(msgSender.SetText(msg) + 1f);
        msgSender.gameObject.SetActive(false);
        action?.Invoke();
    }
}
    
