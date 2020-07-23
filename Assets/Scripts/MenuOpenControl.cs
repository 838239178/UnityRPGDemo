using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpenControl : MonoBehaviour
{
    public Inventory myBag;
    public GameObject inventoryViewer;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (!Input.GetKeyDown(key)) continue;
                switch (key)
                {
                    case KeyCode.Q:
                        inventoryViewer.SetActive(!inventoryViewer.activeSelf);
                        InventoryManager.instance.UpdateContant(myBag); 
                        break;
                    default: break;
                }
            }
        }
    }

    private void Awake()
    {
        //测试
        GameData gmData = Resources.Load<GameData>("GameData");
        ItemEquip[] equips = gmData.equipData;
        foreach (ItemBase iBase in equips)
        {
            myBag.Additem(iBase);
        }
        //
    }
}
