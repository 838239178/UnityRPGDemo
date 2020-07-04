using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject overDialog;

    private GameStatus nowStatus = GameStatus.PlayerAct;

    private List<UnitStats> unitsStats = new List<UnitStats>();

    private GameObject nextUnit = null;

    public GameStatus GetStatusNow() { return nowStatus;  }

    public void SetEscape() { nowStatus = GameStatus.Escaped; }

    public void NextTurn()
    {
        if (OverStatus())  return;
        UnitStats stats = unitsStats[0];
        unitsStats.Remove(stats);
        if (!stats.isDead())
        {
            GameObject unit = stats.gameObject;
            stats.CalculateNextActTurn();
            unitsStats.Add(stats);
            unitsStats.Sort();
            Debug.Log(unit.tag + " acting");
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
        foreach (UnitStats ust in unitsStats)
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
            unitsStats.Add(stats);
        }
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject unit in enemyUnits)
        {
            UnitStats stats = unit.GetComponent<UnitStats>();
            stats.CalculateNextActTurn(0);
            unitsStats.Add(stats);
        }
        unitsStats.Sort();

        playerParty = GameObject.Find("PlayerParty");
        enemyEncounter = GameObject.Find("EnemyEncounter");
        //DontDestroyOnLoad(playerParty);
        //DontDestroyOnLoad(enemyEncounter);

        NextTurn();
    }
}
    
