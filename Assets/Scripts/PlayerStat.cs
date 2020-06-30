using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public string name;
    public uint money;
    public Vector2 scenePosition;

    private void Awake()
    {
        //TO DO; 从存档中读取信息
    }
}
