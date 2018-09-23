using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public AudioSource mainTheme;
    public AudioClip MainthemeSong;

    public GameObject menuMusic; 

    void Start()
    {
        mainTheme.clip = MainthemeSong;
        mainTheme.Play();
        DontDestroyOnLoad(this.menuMusic); 
    }

    public void OnMouseUp()
    {
        SceneManager.LoadScene(1);
    }
}