using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PindahScene: MonoBehaviour {
    AudioSource AudioTombol;
    public AudioClip SFXPencet;

	// Use this for initialization
	void Start () {
        AudioTombol = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GantiScene(string SceneTujuan)
    {
        
        SceneManager.LoadScene(SceneTujuan);
        
    }

    public void KeluarGame()
    {
        Application.Quit();
    }


}
