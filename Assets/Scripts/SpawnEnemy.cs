using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  require NPC or System Cycle placement to place enemy. 
/// </summary>
public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyEncounter;
    public ChangeScene levelLoader;
    [SerializeField]
    private GameObject[] enemyPerfabs;  
    
    public void PutEnemyOnPosition(string enemyName, Vector2 postion)
    {
        //TO DO: radom on enemy perfabs and change its parents to enemyEncounter
        
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
            levelLoader.LoadNextScene("Battle");
        }
    }
}
