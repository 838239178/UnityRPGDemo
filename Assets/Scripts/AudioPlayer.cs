using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip clip;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoad;
        audio.clip = clip;
    }

    public void ChangeMusic(string path)
    {
        //TODO : load audio clip
        clip = Resources.Load<AudioClip>("music/" + path);
        if (clip == audio.clip) return;
        if (clip == null) Debug.LogError("audio clip loaded failed");
        audio.clip = clip;
        audio.Play();
        Debug.Log(clip.name + " loaded success");
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        ChangeMusic(scene.name + "_mic");
    }
}
