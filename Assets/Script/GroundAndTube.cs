//由于地面和水管移动要同步=>放弃单一职责=>高度集成

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAndTube : MonoBehaviour {

    private Data D_ins;           //数据单例

    // Use this for initialization
    void Start () {

        D_ins = Data.getInstance();
        tubeInit(); //水管初始化
        
    }
	
	// Update is called once per frame
	void Update () {
        if(D_ins.birdAlived == true)
        this.GroundAndTubeScroll();
    }

    void tubeInit() //水管初始化
    {
        D_ins.sp_tubeDown1.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x, Random.Range(2.4f, 6.6f));  //朝下水管高度
        D_ins.sp_tubeUp1.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x, D_ins.sp_tubeDown1.transform.position.y - D_ins.TubeUpDownDistance);

        D_ins.sp_tubeDown2.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x + D_ins.TubeLeftRightDistance, Random.Range(2.4f, 6.6f));  //朝下水管高度
        D_ins.sp_tubeUp2.transform.position = new Vector2(D_ins.sp_tubeDown2.transform.position.x, D_ins.sp_tubeDown2.transform.position.y - D_ins.TubeUpDownDistance);
    }

    void GroundAndTubeScroll() //水管&地面=>滚动及刷新
    {
        //=====================地面=====================//
        var ground1Pos = D_ins.sp_ground1.transform.position;
        var ground2Pos = D_ins.sp_ground2.transform.position;

        D_ins.sp_ground1.transform.position = new Vector2(ground1Pos.x -= D_ins.GroundAndTubeSpeed * Time.deltaTime, ground1Pos.y);
        D_ins.sp_ground2.transform.position = new Vector2(ground2Pos.x -= D_ins.GroundAndTubeSpeed * Time.deltaTime, ground2Pos.y);


        if (ground2Pos.x <= -5.237f)
        {
            D_ins.sp_ground2.transform.position = new Vector2(5.237f, ground2Pos.y);
        }
        if (ground1Pos.x <= -5.237f)
        {
            D_ins.sp_ground1.transform.position = new Vector2(5.237f, ground1Pos.y);
        }

        if (D_ins.gameBegin == true)  //游戏开始时才刷新水管
        {
            //=====================水管=====================//
            D_ins.sp_tubeDown1.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x - D_ins.GroundAndTubeSpeed * Time.deltaTime, D_ins.sp_tubeDown1.transform.position.y);
            D_ins.sp_tubeUp1.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x, D_ins.sp_tubeUp1.transform.position.y);
            D_ins.sp_tubeDown2.transform.position = new Vector2(D_ins.sp_tubeDown2.transform.position.x - D_ins.GroundAndTubeSpeed * Time.deltaTime, D_ins.sp_tubeDown2.transform.position.y);
            D_ins.sp_tubeUp2.transform.position = new Vector2(D_ins.sp_tubeDown2.transform.position.x, D_ins.sp_tubeUp2.transform.position.y);


            //===================二者刷新===================//
            if (D_ins.sp_tubeDown1.transform.position.x < -D_ins.TubeDown1X)
            {
                D_ins.sp_tubeDown1.transform.position = new Vector2(D_ins.sp_tubeDown2.transform.position.x + D_ins.TubeLeftRightDistance, Random.Range(2.4f, 6.6f));
                D_ins.sp_tubeUp1.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x, D_ins.sp_tubeDown1.transform.position.y - D_ins.TubeUpDownDistance);
                D_ins.Tube1point = true;
            }
            if (D_ins.sp_tubeDown2.transform.position.x < -D_ins.TubeDown1X)
            {
                D_ins.sp_tubeDown2.transform.position = new Vector2(D_ins.sp_tubeDown1.transform.position.x + D_ins.TubeLeftRightDistance, Random.Range(2.4f, 6.6f));
                D_ins.sp_tubeUp2.transform.position = new Vector2(D_ins.sp_tubeDown2.transform.position.x, D_ins.sp_tubeDown2.transform.position.y - D_ins.TubeUpDownDistance);
                D_ins.Tube2point = true;
            }
        }       

    }

}
