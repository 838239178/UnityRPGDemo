using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public const int PERPAGECOUNT = 8;

    public Text info;
    public GameObject itemContant;
    public List<Item> itemOnContant = new List<Item>();

    private Inventory curBag; 
    private int curBegin = 0;

    public void UpdateInfo(ItemBase item)
    {
        info.text = item.describe;
    }

    private void _DoContantUpdate(int begin)
    {
        for(int i =0; i<this.itemContant.transform.childCount; i++)
        {
            itemOnContant[i].RemoveItem();
        }
        for (int i = begin; i < this.curBag.Items.Count; i++)
        {
            if (i - begin == PERPAGECOUNT) break;
            itemOnContant[i-begin].SetItem(curBag.Items[i]);
        }
    }

    public void UpdateContant(Inventory bag) 
    {
        if(bag)
        {
            if (!curBag || bag != curBag)
            {
                curBag = bag;
                curBegin = 0;
            }
            else
            {
                return;
            }
        }
        if (curBag == null) return;
        this._DoContantUpdate(0);
    }

    public void NextContant()
    {
        if (!curBag) return;
        if(curBegin + PERPAGECOUNT < curBag.Items.Count) curBegin += PERPAGECOUNT;
        this._DoContantUpdate(curBegin);
    }

    public void PrevContant()
    {
        if (!curBag) return;
        if(curBegin >= PERPAGECOUNT) curBegin -= PERPAGECOUNT;
        this._DoContantUpdate(curBegin);
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
                {
                    if (scene.name == "Battle")
                        this.gameObject.SetActive(false);
                    else
                        this.gameObject.SetActive(true);
                };
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < this.itemContant.transform.childCount; i++)
        {
            itemOnContant.Add(this.itemContant.transform.GetChild(i).GetComponent<Item>());
        }
    }
}
