using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 用于响应战斗界面button的事件
/// </summary>
class SelectUnit : MonoBehaviour
{
    private GameObject currentUnit;

    private GameObject actionsMenu, enemyUnitsMenu;    
    
    private GameObject skillLayer;

    private TurnSystem turnSys;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Update()
    {
        if(turnSys.GetStatusNow() == GameStatus.PlayerAct)
        {
            //鼠标右键返回上一步
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                if(!actionsMenu.activeSelf && enemyUnitsMenu.activeSelf)
                {
                    enemyUnitsMenu.SetActive(false);
                    actionsMenu.SetActive(true);
                }
                if(!actionsMenu.activeSelf && skillLayer.activeSelf)
                {
                    skillLayer.SetActive(false);
                    actionsMenu.SetActive(true); 
                }
            }
        }    
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Battle")
        {
            this.actionsMenu = GameObject.Find("ActionsMenu");
            this.enemyUnitsMenu = GameObject.Find("EnemyUnitsMenu");
            this.skillLayer = GameObject.Find("SkillLayer");
            this.turnSys = GameObject.Find("TurnSystem").GetComponent<TurnSystem>();
        }
    }

    public void SetCurrentUnit(GameObject unit)
    {
        this.currentUnit = unit;
        this.actionsMenu.SetActive(true);
        this.enemyUnitsMenu.SetActive(false);
        this.skillLayer.SetActive(false);
        Debug.Log("Set currentUnit success");
    }

    public void SelectAction(ActionType type)
    {
        this.currentUnit.GetComponent<PlayerUnitAction>().actionType = type;
        
        this.actionsMenu.SetActive(false);

        if (type == ActionType.MagicalSelect)
        {
            //TO DO: 设置技能选择面板为active
            this.skillLayer.SetActive(true);
        }
        else
        {
            this.enemyUnitsMenu.SetActive(true);
        }

    } 

    public void SelectSkill(string name)
    {
        //TO DO: 根据转递回来的参数设置PlayerUnitAction中的技能名
        this.currentUnit.GetComponent<PlayerUnitAction>().skillName = name;
    }

    public void AttackEnemy(GameObject target)
    {
        this.enemyUnitsMenu.SetActive(false);
        this.currentUnit.GetComponent<PlayerUnitAction>().Act(target);
    }
}
