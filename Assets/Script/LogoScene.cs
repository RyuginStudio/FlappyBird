using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("MainScene");
    }
}
