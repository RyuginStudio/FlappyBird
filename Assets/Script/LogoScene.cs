using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour {

    public AudioSource vszed;

	void Awake()
	{
		//锁帧:60 只在垂直同步为关的情况下奏效
		Application.targetFrameRate = 60;
	}

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
