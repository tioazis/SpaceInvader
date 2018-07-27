using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoControl : MonoBehaviour {

    public static SerialPort ArduPort = new SerialPort("COM6", 9600);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
