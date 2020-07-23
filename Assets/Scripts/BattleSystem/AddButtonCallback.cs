using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddButtonCallback : MonoBehaviour
{
    [SerializeField]
    private ActionType type;
    // Start is called before the first frame update
    void Start()
    {
        
        Button btn = this.GetComponent<Button>();
        SelectUnit selectUnit = GameObject.Find("PlayerParty").GetComponent<SelectUnit>();
        btn.onClick.AddListener(
            delegate 
            { 
                selectUnit.SelectAction(type); 
            });
    }

}
