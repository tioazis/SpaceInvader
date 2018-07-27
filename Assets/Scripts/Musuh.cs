using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musuh : MonoBehaviour {
    public int delay;
    public float Kecepatan;
    GameManager GM;

    private Collider2D batasKanan;
    private Collider2D batasKiri;

    public GameObject peluruMusuh;

    float cur = 0;//variabel pembantu untuk deteksi delay tembakan
    bool tembak = false;
    AudioSource AudioMusuh;
    public AudioClip MusuhTembak;
    public AudioClip MusuhTertembak;
    
	// Use this for initialization
	void Start () {
        batasKanan = GameObject.Find("BatasKananMusuh").GetComponent<BoxCollider2D>();
        batasKiri = GameObject.Find("BatasKiriMusuh").GetComponent<BoxCollider2D>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        AudioMusuh = this.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        AutoGerak();
        if(!tembak)
           cur += Time.deltaTime;

        if (cur >= delay)
        {
            cur = 0;
            tembak = true;
        }
        if (tembak)
        {
            this.AutoTembak();
        }
	}

    void OnTriggerEnter2D(Collider2D cols)
    {
        if (cols.gameObject.tag=="BatasMusuh")
        {
            if (GM.TabrakKanan)
            {

                batasKanan.enabled = false;
                batasKiri.enabled = true;
              // transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
                GM.TabrakKanan = false;
                ///print("sampah");
            }
            else
            {
                batasKanan.enabled = true;
                batasKiri.enabled = false;
               // transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
                GM.TabrakKanan = true;
            }
        }
    }

    void TembakPlayer()
    {
        Instantiate(peluruMusuh,transform.position,Quaternion.identity);
        AudioMusuh.PlayOneShot(MusuhTembak);
        
    }

    void TembakAktif()
    {
        this.tembak = true;
    }

    void AutoGerak()
    {
        if (GM.TabrakKanan)
        {
            this.transform.Translate(Vector2.right * Kecepatan * Time.deltaTime);

        }
        else
        {
            this.transform.Translate(Vector2.right * -Kecepatan * Time.deltaTime);
        }
    }
    void AutoTembak()
    {
        if (tembak)
        {

            this.TembakPlayer();
            this.tembak = false;          
            return;
        }
    }

    public void MusuhHurt()
    {
        AudioMusuh.PlayOneShot(MusuhTertembak);
    }
}


