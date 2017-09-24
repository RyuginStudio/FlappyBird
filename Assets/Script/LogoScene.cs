using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScene : MonoBehaviour {

    public AudioSource vszed;

	// Use this for initialization
	void Start () {
        Invoke("playSound", 0.5f);
        Invoke("jumpNextScene", 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void playSound()
    {
        vszed.Play();
    }
    void jumpNextScene()
    {
        Application.LoadLevel("MainScene");
    }
}
