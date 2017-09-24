//部分数据采用硬编码

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour {

    private int bird_idx = 0;     //鸟帧动画
    private float currentTime;    //当前时间 
    private float lastTime;       //上次时间
    private Data D_ins;           //数据单例

    private static Bird birdInstance;   //鸟单例

    private Bird()
    {

    }

    public static Bird getInstance()   //u3d特殊单例模式
    {
        return birdInstance;
    }

    void Awake()
    {
        birdInstance = this;
    }

    void Start () {
        lastTime = Time.time;
        D_ins = Data.getInstance();

        D_ins.now_score2.active = false;
        D_ins.now_score3.active = false;
        D_ins.now_score4.active = false;
    }
	
	void Update () {

        birdAnimation();
        birdControl();
    }  

     void birdAnimation()  //鸟动画
    {
        if (D_ins.birdAlived == true)
        {
            currentTime = Time.time;

            if (currentTime - lastTime > 0.12)   //简易定时器
            {
                Texture2D texture2d = (Texture2D)Resources.Load("Pictures/bird_blue_" + bird_idx);  //更换角色图片  
                var sprite = Sprite.Create(texture2d, D_ins.sp_bird.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f));
                D_ins.sp_bird.GetComponent<SpriteRenderer>().sprite = sprite;

                bird_idx = bird_idx >= 2 ? bird_idx = 0 : ++bird_idx;
                lastTime = currentTime;
            }
        }
    }

    void birdRotation()  //鸟静态旋转动作
    {
        if (D_ins.sp_bird.transform.rotation.z > -0.71)
        {
            //Debug.Log(D_ins.sp_bird.transform.rotation.z);
            D_ins.sp_bird.transform.Rotate(new Vector3(0, 0, -110 * Time.deltaTime));
        }
    }

     void birdControl() //用户操控
    {
        if (Input.GetMouseButtonDown(0) && D_ins.birdAlived == true)
        {
            D_ins.audio_swing.Play(); //播放音效

            //向上旋转
            D_ins.sp_bird.transform.rotation = Quaternion.Euler(0, 0, 30); //欧拉角

            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            if(this.transform.position.y < 5.5) //防止小鸟飞出上边界
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
        }
        else if(D_ins.sp_bird.transform.position.y > -3) //可下旋转临界值
        {
            birdRotation();
        }
    }
     
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (D_ins.birdAlived == true)
        {
            D_ins.audio_hit.Play();

            if (this.transform.position.y > -2.3)
            {
                Invoke("playDieAudio", 0.4f);
            }
        }

        D_ins.birdAlived = false;

        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        foreach (var item in GameObject.FindGameObjectsWithTag("tube"))
        {
            item.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Invoke("refreshScene", 3);

    }

    void playDieAudio()
    {
        D_ins.audio_die.Play(); //死亡坠落声
    }

    void refreshScene()  //游戏结束刷新场景
    {
        SceneManager.LoadScene("MainScene");
        D_ins.Tube1point = true;  //重置开关
        D_ins.Tube2point = true;
        D_ins.birdAlived = true;
        D_ins.Nowscore = 0;
        D_ins.now_score2.active = false;
        D_ins.now_score3.active = false;
        D_ins.now_score4.active = false;
    }

}
