using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] Animator anim;
    public float translationTime = 1f;

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        anim.SetTrigger("start");
        yield return new WaitForSeconds(translationTime);
        SceneManager.LoadScene(sceneName);
    }

}
