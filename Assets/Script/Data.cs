using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

    //音效(有默认值的)
    public AudioSource audio_hit;
    public AudioSource audio_swing;
    public AudioSource audio_die;
    public AudioSource audio_point;
    public AudioSource audio_swooshing;

    //精灵
    public GameObject sp_ground1;
    public GameObject sp_ground2;
    public GameObject sp_tubeDown1;
    public GameObject sp_tubeDown2;
    public GameObject sp_tubeUp1;
    public GameObject sp_tubeUp2;
    public GameObject sp_bird;
    public GameObject now_score1; //四位数字
    public GameObject now_score2;
    public GameObject now_score3;
    public GameObject now_score4;

    //数据(括号内为默认值)
    public int Nowscore;                 //当前游戏得分（0）
    public int GroundAndTubeSpeed;       //地面&水管移动速度（2）
    public float TubeDown1X;             //水管1朝下X坐标（3.3）
    public float TubeDown2X;             //水管1朝下X坐标（6.3）
    public float TubeUpDownDistance;     //水管上下间距（7.6）
    public float TubeLeftRightDistance;  //水管左右间距（3.5）
    public float TubeDownMinHeight;      //朝下水管高度要大于（2.4）
    public float TubeDownMaxHeight;      //朝下水管高度要小于（6.6）

    //开关
    public bool Tube1point;  //能否得分
    public bool Tube2point;  //能否得分
    public bool birdAlived;  //是否存活

    //数据单例
    private static Data gameData;

    public static Data getInstance()   //u3d特殊单例
    {
        return gameData;
    }

    private Data()
    { 
   
    }

    void Awake()
    {
        gameData = this;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

   
}
