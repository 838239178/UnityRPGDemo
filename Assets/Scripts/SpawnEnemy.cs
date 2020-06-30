using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyEncounter;

    private void Awake()
    {
        enemyEncounter = GameObject.Find("EnemyEncounter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (enemyEncounter.transform.childCount == 0)
        {
            Debug.LogWarning("No enemy under this encounter");
            return;
        }

        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("Battle");
        }
    }
}
