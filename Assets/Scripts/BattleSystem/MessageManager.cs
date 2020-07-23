using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private Text message;

    [SerializeField]
    private bool isPlay = false;

    [SerializeField]
    private int speed = 50;

    private int index = 0;

    private string curStr = "";

    private float passDt = 0;

    private float timeLine = 0;

    public float SetAtkText(string owner, string target, float damage, bool isMagic)
    {
        if (isPlay) return 0f;
        this.gameObject.SetActive(true);
        string atkType = isMagic ? "魔法攻击" : "物理攻击";
        int damageInt = Mathf.FloorToInt(damage);
        curStr = /*"<b>" +*/ owner /*+ "</b>"*/ 
            + "使用了" + atkType 
            /*+ "<b>"*/ + target/* + "</b>"*/ 
            + "受到了" 
           /* + "<color=red><i>" */+ damageInt /*+ "</color></i>"*/ + "点伤害";
        isPlay = true;
        timeLine = 1.0f / speed;
        return timeLine;
    }

    public float SetText(string str)
    {
        if (isPlay) return 0f;
        this.gameObject.SetActive(true);
        curStr = str;
        isPlay = true;
        timeLine = 1.0f / speed;
        return timeLine;
    }

    private void OnDisable()
    {
        message.text = "";
        curStr = "";
        isPlay = false;
        index = 0;
        passDt = 0;
    }

    private void Update()
    {
        if (isPlay)
        {
            if (index >= curStr.Length)
            {
                isPlay = false;
                //this.gameObject.SetActive(false);
            }
                
            else if (passDt >= timeLine)
            {
                message.text += curStr[index++];
                passDt = 0f;
            }
            passDt += Time.deltaTime;
        }
    }
}
