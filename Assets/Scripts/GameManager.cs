using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Text txtScore;
	public Text txtHealth;
	public int Score=0;
	public int Health=3;

    AudioSource AudioGame;
    public AudioClip HeroMeledak;
    public AudioClip GameOver;
    

    //timer
    public float Waktu;
	public Text txtWaktu;
	public bool Hitung;

    //variabel pause
    private bool paused;
    public GameObject PauseMark;

    public bool TabrakKanan = true;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
		setScore (0);
		setHealth (0);

        AudioGame = this.GetComponent<AudioSource>();

		//timer
		Waktu = 30;
		Hitung = false;
		StartCoroutine (DelayDikit ());
	}
	
	// Update is called once per frame
	void Update () {

		//timer
		if (Hitung == true) {
			Waktu -= Time.deltaTime;
			txtWaktu.text = Waktu.ToString ("f0");
			//Debug.Log ("mulai menghitung");
            if(Waktu<=1)
            {
                
                PlayerPrefs.SetInt("NilaiSementara", Score);
                PlayerPrefs.SetInt("WaktuSisa", (int)Waktu);
                Invoke("SceneResult", 2f);
                
                
            }
		}


        if (Input.GetKey(KeyCode.Escape))
        {
            setPause();
        }
	}

	public void setScore(int nilai){
		Score += nilai;
		txtScore.text = Score.ToString ();
	}

	public void setHealth(int Hp){
		Health -= Hp;
		txtHealth.text = Health.ToString ();
		if (Health <= 0) {
			Destroy (GameObject.Find ("HERO"));

            
            AudioGame.PlayOneShot(HeroMeledak);
            Invoke("SceneResult", 1f);
            PlayerPrefs.SetInt("NilaiSementara", Score);
            PlayerPrefs.SetFloat("WaktuSisa", (int)Waktu);
        }
	}
 


	void SceneResult(){
        
        Time.timeScale = 0f;
		SceneManager.LoadScene ("Result");
        
	}

	IEnumerator DelayDikit(){
		yield return new WaitForSeconds(1f);
		Hitung = true;
	}

	//pause Game
	public void setPause(){
        PauseResume();
	}

    void OnApplicationPause(bool pauseStatus)
    {
        paused = pauseStatus;
    }

    public void PauseResume()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            OnApplicationPause(true);
        }
        else
        {
            Time.timeScale = 1;
            OnApplicationPause(false);
        }
    }


}
