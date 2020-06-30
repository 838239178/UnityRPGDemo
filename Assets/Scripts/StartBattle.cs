using System.Collections;
using System.Collections.Generic;
using UnityEngine ;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{
    public static StartBattle current = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if(current != null)
        {
            Debug.Log("Destory success");
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Battle")
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Title")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(scene.name == "Battle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
