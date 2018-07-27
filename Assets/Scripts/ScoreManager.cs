using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    GameManager GMScore;

    private int ScoreSementara;
    private int WaktuSisa;
    private int ScoreAkhir;

    AudioSource AudioResult;
    public AudioClip MenangSfx;

    public Text TxtScoreAkhir;

	// Use this for initialization
	void Start () {
        AudioResult = this.GetComponent<AudioSource>();
        ScoreSementara = PlayerPrefs.GetInt("NilaiSementara");
        WaktuSisa = PlayerPrefs.GetInt("WaktuSisa");
        AudioResult.PlayOneShot(MenangSfx);

    }
	
	// Update is called once per frame
	void Update () {
        ScoreAkhir = ScoreSementara + WaktuSisa * 10;
        TxtScoreAkhir.text = ScoreAkhir.ToString();
        
	}
}
