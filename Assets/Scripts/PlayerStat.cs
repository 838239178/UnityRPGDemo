using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public string userName;
    public uint money;
    public string sceneName;
    public Vector2 scenePosition;

    private void Awake()
    {
        //TO DO; 从存档中读取信息
    }
}
