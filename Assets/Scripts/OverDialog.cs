using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverDialog : MonoBehaviour
{
    public GameStatus onEnableStatus;

    private void OnEnable()
    {
        Text dialogText = this.transform.GetChild(1).GetComponent<Text>();
        Button dialogBtn = this.transform.GetChild(0).GetComponent<Button>();
        if(onEnableStatus == GameStatus.Win)
        {
            dialogText.text = "Victory";
            WinnerReward();
        }
        else if (onEnableStatus == GameStatus.Lose)
        {
            dialogText.text = "Defeat";
            LoserPunish();
        }
        else
        {
            dialogText.text = "Escaped";
        }
    }

    private void WinnerReward()
    {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<PlayerStat>().money += 100;
    }

    private void LoserPunish()
    {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<PlayerStat>().money -= 100;
    }
}
