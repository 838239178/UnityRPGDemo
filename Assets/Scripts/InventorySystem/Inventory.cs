using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "creat", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    private List<ItemBase> items = new List<ItemBase>(); 
    public List<ItemBase> Items { get => items;}

    public void Additem(ItemBase item)
    {
        //if (Items.Contains(item))
        //{
        //    item.itemHeiled++;
        //}
        //else
        //{
        //    Items.Add(item);
        //}
        items.Add(item);
    }
}
