using UnityEngine;
using UnityEngine.UI;

class CreatEnemyMenuItem : MonoBehaviour
{
    public Object TargetEnmeyUnitPrefab;
    public Sprite MenuItemSprite;
    public Vector3 InitialPosition;
    public Vector2 ItemDimensions;
    public KillEnemy KillEnemyScript;

    //private SelectUnit selectUnit; 

    private void OnEnable()
    {
        GameObject menu = GameObject.Find("EnemyUnitsMenu");
        if (menu == null) return;
        GameObject newIns = (GameObject)Instantiate(TargetEnmeyUnitPrefab, menu.transform);
        //TO DO; 根据 enemy item 的数量计算位置
        newIns.transform.position = menu.transform.TransformPoint(InitialPosition);
        newIns.GetComponent<Button>().onClick.AddListener(
            delegate() 
            { 
                this.OnSelectEnemy(); 
            });
        KillEnemyScript.MenuItem = newIns;
    }

    private void OnSelectEnemy()
    {
        GameObject party = GameObject.Find("PlayerParty");
        party.GetComponent<SelectUnit>().AttackEnemy(this.gameObject);
        Debug.Log("select " + this.name);
    }
}
