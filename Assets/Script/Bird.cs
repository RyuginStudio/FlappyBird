//部分数据采用硬编码

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{

	private int bird_idx = 0;     //鸟帧动画
	private float currentTime;    //当前时间 
	private float lastTime;       //上次时间
	private Data D_ins;           //数据单例
	bool moveRoam = false;        //漫游上下开关


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

	void Start()
	{
		lastTime = Time.time;
		D_ins = Data.getInstance();

		D_ins.now_score2.SetActive(false);
		D_ins.now_score3.SetActive(false);
		D_ins.now_score4.SetActive(false);
	}

	void Update()
	{

		birdAnimation();

		if (D_ins.gameBegin == true)
		{
			birdControl();
		}
		else
		{
			birdRoam();  //漫游
		}
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

	void birdRoam()  //未开始游戏时漫游
	{
		if (D_ins.sp_bird.transform.position.y < -0.45)
			moveRoam = true;   //上移        
		else if (D_ins.sp_bird.transform.position.y > 0.5)
			moveRoam = false;  //下移

		var tempPos = D_ins.sp_bird.transform.position;

		if (moveRoam == false)
			D_ins.sp_bird.transform.position = new Vector2(tempPos.x, tempPos.y -= 0.005f);
		else
			D_ins.sp_bird.transform.position = new Vector2(tempPos.x, tempPos.y += 0.005f);

		if (Input.GetMouseButtonDown(0))  //获取游戏开始指令
		{
			D_ins.gameBegin = true; //游戏开始
			D_ins.sp_bird.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

			//以下来自birdControl函数
			D_ins.audio_swing.Play();
			D_ins.sp_bird.transform.rotation = Quaternion.Euler(0, 0, 30); //欧拉角
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);

			//隐藏UI元素
			D_ins.getReady.SetActive(false);
			D_ins.tip.SetActive(false);
		}
	}

	void birdRotation()  //鸟静态旋转动作
	{
		if (D_ins.sp_bird.transform.rotation.z > -0.71)
		{
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

			if (this.transform.position.y < 5.5) //防止小鸟飞出上边界
				this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
		}
		else if (D_ins.sp_bird.transform.position.y > -3) //可下旋转临界值
		{
			birdRotation();
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (D_ins.birdAlived == true)
		{
			D_ins.audio_hit.Play();

			//需要立刻让鸟不模拟=>模拟
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

			Invoke("scorePanelDisplay", 1.5f);

			if (this.transform.position.y > -2.3)
			{
				Invoke("playDieAudio", 0.4f);
			}
		}

		D_ins.birdAlived = false;

		foreach (var item in GameObject.FindGameObjectsWithTag("tube"))
		{
			item.GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}

	void playDieAudio()
	{
		D_ins.audio_die.Play(); //死亡坠落声
	}

	public void scorePanelDisplay() //计分板显示
	{
		D_ins.now_score1.SetActive(false);
		D_ins.now_score2.SetActive(false);
		D_ins.now_score3.SetActive(false);
		D_ins.now_score4.SetActive(false);

		D_ins.gameover.SetActive(true);
		D_ins.btnOk.SetActive(true);
		D_ins.scorePanel.SetActive(true);

		D_ins.audio_swooshing.Play();


		//分数显示 => 分数父节点：缩放、移动、更改childAlignment
		D_ins.now_score1.GetComponentInParent<RectTransform> ().localScale = new Vector2 (0.5f, 0.5f);
		D_ins.now_score1.GetComponentInParent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
		D_ins.now_score1.GetComponentInParent<RectTransform>().position = new Vector2(1.245f, 0.413f);

		for (int i = 1; i <= D_ins.Nowscore.ToString().ToCharArray().Length; i++) 
		{
			switch (i)
			{
			case 1:
				D_ins.now_score1.SetActive(true);
				break;
			case 2:
				D_ins.now_score2.SetActive(true);
				break;
			case 3:
				D_ins.now_score3.SetActive(true);
				break;
			case 4:
				D_ins.now_score4.SetActive(true);
				break;
			}
		}

		//奖杯显示: 铜=>10+; 银=>20+; 金=>30+; 铂金=>40+;
		Sprite sp_medal;
		Texture2D tex_medal;
		string path_medal = null;
		if (D_ins.Nowscore >= 40) {
			path_medal = "platinum";
		} else if (D_ins.Nowscore >= 30) {
			path_medal = "gold";
		} else if (D_ins.Nowscore >= 20) {
			path_medal = "silver";
		} else if (D_ins.Nowscore >= 10) {
			path_medal = "bronze";
		}

		if (path_medal != null) {
			tex_medal = (Texture2D)Resources.Load("Pictures/"+path_medal);
			sp_medal = Sprite.Create(tex_medal, D_ins.medal.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f));
			D_ins.medal.GetComponent<SpriteRenderer>().sprite = sp_medal;
			D_ins.medal.SetActive(true);
		}

			
	}


	public void btn_ok_onClick()
	{
		D_ins.audio_swooshing.Play();
		Invoke("refreshScene", 0.7f);
		D_ins.btnOk.GetComponent<Button>().interactable = false;  //按钮不可点击
		//D_ins.btnOk.SetActive(false);  //按钮隐藏
	}

	public void refreshScene()  //游戏结束刷新场景
	{
		SceneManager.LoadScene("MainScene");
		D_ins.Tube1point = true;  //重置开关
		D_ins.Tube2point = true;
		D_ins.birdAlived = true;
		D_ins.gameBegin = false;

		D_ins.Nowscore = 0;
	}

}
