using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SkillCell : MonoBehaviour
{ 
    private Skill data;
    public Skill Data
    {
        get => data;
        set
        {
            data = value;
            Init();
        }
    }

    public Animator anim;
    public AudioSource audio;

    [SerializeField] private GameObject skillName;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject describe;
    [SerializeField] private GameObject cost;
    
    private void Init()
    {
        icon.GetComponent<Image>().sprite = Resources.Load<SpriteAtlas>(data.iconAtlas).GetSprite(data.icon);
        describe.GetComponent<Text>().text = data.describe;
        skillName.GetComponent<Text>().text = "<color=red>" + data.skillName + "</color>";
        cost.GetComponent<Text>().text = "<color=yellow>" + data.cost + "MP" + "</color>";
        this.GetComponent<Button>().onClick.AddListener(
            delegate
            {
                OnButtonClick();
            });
        audio.clip =  Resources.Load<AudioClip>(data.sound);
    }

    public void OnButtonClick()
    {
        GameObject.Find("PlayerParty").GetComponent<SelectUnit>().SelectSkill(this);
        Debug.Log("selected " + Data.skillName);
    }
}
