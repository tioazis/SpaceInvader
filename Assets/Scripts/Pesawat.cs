using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Pesawat : MonoBehaviour {
    public float Kecepatan;

	public GameObject PeluruPlayer;
	public GameObject PosisiPeluruKanan;
	public GameObject PosisiPeluruKiri;


    public AudioSource AudioHero;
    public AudioClip TembakHero;
    public AudioClip HeroTertembak;

    public static SerialPort ArduPort = new SerialPort("COM6", 9600);

    public string BtnStats;

    //variabel batas kanan kiri
    bool Batas = true;
	private Vector2 posisiMin, posisiMax;

	//variabel delaying shoot
	bool Tembak = true;


	// Use this for initialization
	void Start () {
        //deklarasi batasan
        OpenConnection();

		posisiMin = Camera.main.ScreenToWorldPoint(new Vector2(0,0));
		posisiMax = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));
		print (Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width/2, Screen.height)));
	}
	
	// Update is called once per frame
	void Update () {

        //GerakTerbatas ();
        //Tembakin();
        //AutoTembak();
        //TouchControl();
        BtnStats = DigitalRead(ArduPort);
            
        print(BtnStats);

        ControlArduino(BtnStats);
    }



	void TembakMusuh(){
		GameObject Peluru1 = (GameObject)Instantiate (PeluruPlayer);
		Peluru1.transform.position = PosisiPeluruKanan.transform.position;

		GameObject Peluru2 = (GameObject)Instantiate (PeluruPlayer);
		Peluru2.transform.position = PosisiPeluruKiri.transform.position;
        AudioHero.PlayOneShot(TembakHero);

	} //fungsi untuk spawn peluru pada empty objek

	void AktifkanTembak(){
		Tembak = true;
	}

	void GerakTerbatas(){

		//limited movement
		//Gerak Atas
		/*if (Input.GetKey (KeyCode.W)) 
		{

			if (Batas) 
			{
				Vector2 posisiBaru = (Vector2)this.transform.position + Vector2.up * Kecepatan * Time.deltaTime;
				if (posisiBaru.y < posisiMax.y) {
					this.transform.position = posisiBaru;
				}
			} else 
			{
				this.transform.Translate (new Vector2 (0, 10) * Kecepatan * Time.deltaTime);
			}
		}*/
		//Gerak Bawah
		/*if (Input.GetKey (KeyCode.S)) 
		{

			if (Batas) 
			{
				Vector2 posisiBaru = (Vector2)this.transform.position - Vector2.up * Kecepatan * Time.deltaTime;
				if (posisiBaru.y > posisiMin.y) {
					this.transform.position = posisiBaru;
				}
			}
		}*/
		//Gerak Kiri
		if (Input.GetKey (KeyCode.A)) 
		{

			if (Batas) 
			{
				Vector2 posisiBaru = (Vector2)this.transform.position - Vector2.right * Kecepatan * Time.deltaTime;
				if (posisiBaru.x > posisiMin.x+0.3f) {
					this.transform.position = posisiBaru;
				}
			}
		}
		//Gerak Kanan
		if (Input.GetKey (KeyCode.D)) 
		{

			if (Batas) 
			{
				Vector2 posisiBaru = (Vector2)this.transform.position + Vector2.right * Kecepatan * Time.deltaTime;
				if (posisiBaru.x < posisiMax.x-0.3f) {
					this.transform.position = posisiBaru;
				}
			}

			Debug.Log ("D");
		}

	}// pergerakan menggunakan batasan seperti border

    void GerakTakTerbatas()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * Kecepatan * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.up * -Kecepatan * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.right * -Kecepatan * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Kecepatan * Time.deltaTime);
        }
    } //Pergerakan tanpa batasan posisi x dan y

    void Tembakin()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Tembak)
            {
                TembakMusuh();
                Tembak = false;
                Invoke("AktifkanTembak", 0.5f);
                return;
            }
        }
    }//fungsi untuk melakukan input tombol spasi

    void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                if (Batas)
                {
                    Vector2 posisiBaru = (Vector2)this.transform.position - Vector2.right * Kecepatan * Time.deltaTime;
                    if (posisiBaru.x > posisiMin.x + 0.2f)
                    {
                        this.transform.position = posisiBaru;
                    }
                }
            }
            else if (touch.position.x > Screen.width / 2)
            {
                if (Batas)
                {
                    Vector2 posisiBaru = (Vector2)this.transform.position + Vector2.right * Kecepatan * Time.deltaTime;
                    if (posisiBaru.x < posisiMax.x)
                    {
                        this.transform.position = posisiBaru;
                    }
                }
            }
        }
    }//fugsi untuk menggunkanan touch layar untuk menggerakkan

    void AutoTembak()
    {
        if (Tembak)
        {
            TembakMusuh();
            Tembak = false;
            Invoke("AktifkanTembak", 1.2f);
            return;
        }
    }

    public void AudioHurt()
    {
        AudioHero.PlayOneShot(HeroTertembak);
    }
    public void OpenConnection()
    {
        if (ArduPort != null)
        {
            if (ArduPort.IsOpen)
            {
                ArduPort.Close();
                print("Koneksi diputus, sebelumnya sudah dibuka");
            }
            else
            {
                ArduPort.Open();
                ArduPort.ReadTimeout = 50;
                print("Membuka Koneksi");
            }
        }
        else
        {
            if (ArduPort.IsOpen)
            {
                print("Koneksi sudah dibuka");
            }
            else
            {
                print("Koneksi eror, tidak ada arduino yang terbaca");
            }
        }
    }
    public string DigitalRead(SerialPort ardu)
    {
        try
        { return ardu.ReadLine(); }
        catch
        { return null; }
    }

    public void ControlArduino(string stat)
    {
        if (stat == "1")
        {
            if (Batas)
            {
                Vector2 posisiBaru = (Vector2)this.transform.position - Vector2.right * Kecepatan * Time.deltaTime;
                if (posisiBaru.x > posisiMin.x + 0.3f)
                {
                    this.transform.position = posisiBaru;
                }
            }

        }
        else if (stat == "2")
        {
            if (Batas)
            {
                Vector2 posisiBaru = (Vector2)this.transform.position + Vector2.right * Kecepatan * Time.deltaTime;
                if (posisiBaru.x < posisiMax.x - 0.3f)
                {
                    this.transform.position = posisiBaru;
                }
            }
        }
        else if (stat == "3")
        {
            if (Tembak)
            {
                TembakMusuh();
                Tembak = false;
                Invoke("AktifkanTembak", 0.5f);
                return;
            }

        }

    }
}