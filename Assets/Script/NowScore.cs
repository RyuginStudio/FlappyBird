using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowScore : MonoBehaviour {

    private Data D_ins;           //数据单例


    // Use this for initialization
    void Start () {
        D_ins = Data.getInstance();
    }
	
	// Update is called once per frame
	void Update () {
        getScore();
    }

    void getScore()   //得分函数
    {
        if (D_ins.sp_tubeDown1.transform.position.x <= -0.97 && D_ins.Tube1point == true)
        {
            D_ins.audio_point.Play();  //得分音效
            D_ins.Tube1point = false;  //得分开关控制
            ++D_ins.Nowscore;

            refresh(); //刷新分数显示
        }
        else if (D_ins.sp_tubeDown2.transform.position.x <= -0.97 && D_ins.Tube2point == true)
        {
            D_ins.audio_point.Play();
            D_ins.Tube2point = false;
            ++D_ins.Nowscore;

            refresh();
        }
    }

    void refresh()  //刷新并显示得分
    {
        char []arr_score = D_ins.Nowscore.ToString().ToCharArray();

        for (int i = 0; i < arr_score.Length; i++)
        {
            Texture2D texture2d = (Texture2D)Resources.Load("Pictures/" + arr_score[i]);  //更换角色图片  
            var sprite = Sprite.Create(texture2d, D_ins.now_score1.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f));
            
            switch (i)  //改位值
            {
                case 0:
				    D_ins.now_score1.SetActive(true);
                    D_ins.now_score1.GetComponent<SpriteRenderer>().sprite = sprite;
                    break;
                case 1:
				    D_ins.now_score2.SetActive(true);
                    D_ins.now_score2.GetComponent<SpriteRenderer>().sprite = sprite;
                    break;
                case 2:
				    D_ins.now_score3.SetActive(true);
                    D_ins.now_score3.GetComponent<SpriteRenderer>().sprite = sprite;
                    break;
                case 3:
				    D_ins.now_score4.SetActive(true);
                    D_ins.now_score4.GetComponent<SpriteRenderer>().sprite = sprite;
                    break;
            }
        }
    }

}
