using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, ISelectHandler
{
    private ItemBase data;
    public ItemBase Data
    {
        get => data;
    }

    public bool invalid = false;
    public Button thisBtn;
    public GameObject thisIcon;
    public Animator anim;

    public GameObject selecter;
    public GameObject details;

    private bool showDetils = false;

    private Vector2 ItemBoxSize;
    
    private void Start()
    {
        thisBtn.onClick.AddListener(delegate { InventoryManager.instance.UpdateInfo(this.data); });
        ItemBoxSize = this.GetComponent<RectTransform>().sizeDelta;
    }

    private void Update()
    {
        if (!invalid)
        {
            if (IsCursorOnThis())
            {
                if(showDetils == false)
                {
                    details.SetActive(true);
                    details.GetComponent<Animator>().SetTrigger("enter");
                    details.transform.GetChild(0).GetComponent<Text>().text =
                        data.itemName + "\n"
                        + "品质：" + ((ItemEquip)data).quality.ToString() + "\n"
                        + "价值：" + "<color=yellow>" + data.value + "</color>"; 
                }
                details.transform.position = Input.mousePosition;
                showDetils = true;
            }
            else if (showDetils == true)
            {
                //Debug.Log(this.name + " close details");
                details.GetComponent<Animator>().SetTrigger("exit");
                showDetils = false;
            }
        }

    }

    private bool IsCursorOnThis()
    {
        Vector2 cursorPos = Input.mousePosition;
        Vector2 thisPos = this.transform.position;
        float width = this.ItemBoxSize.x;
        float height = this.ItemBoxSize.y;

        if (cursorPos.x > thisPos.x + width / 2
            || cursorPos.x < thisPos.x - width / 2)
            return false;
        if (cursorPos.y > thisPos.y + height / 2
            || cursorPos.y < thisPos.y - height / 2)
            return false;

        return true;
    }

    public void SetItem(ItemBase item)
    {
        anim.SetTrigger("enter");
        this.data = item;
        this.invalid = false;
        this.thisBtn.interactable = true;
        this.thisIcon.SetActive(true);
        this.thisIcon.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>("Icons/" + data.iconAtlas).GetSprite(data.icon);
    }

    public void RemoveItem()
    {
        this.data = null;
        this.thisBtn.interactable = false;
        this.invalid = true;
        this.thisIcon.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        this.selecter.transform.parent = this.transform;
        this.selecter.transform.localPosition = Vector3.zero;
    }
}
