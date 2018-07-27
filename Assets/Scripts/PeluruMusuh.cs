using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeluruMusuh : MonoBehaviour {

	public float kecepatan;

    AudioSource AudioPeluru;
    public AudioClip PesawatKena;
    

	// Use this for initialization
	void Start () {
		kecepatan = 3f;
        AudioPeluru = this.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position; 

		position = new Vector2 (position.x, position.y - kecepatan * Time.deltaTime);
		transform.position = position;

		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,-0.5f));

		if(transform.position.y<max.y)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D cols)
	{
		if (cols.tag == "Player") {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().setHealth(1);
            GameObject.Find("HERO").GetComponent<Pesawat>().AudioHurt();
            Destroy (this.gameObject);
            
		}
	}
}