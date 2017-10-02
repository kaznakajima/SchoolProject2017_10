using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    // ---UFOの座標---
    // UFOの原点
    Vector3 ufoOrigin;
    // UFOの上下移動
    Vector3 ufoPos;
    // UFOの左右移動
    Vector3 ufoMove;

    // ---UFOの移動---
    // 移動スピード
    public float speed;
    // --通常移動--
    //  左右移動判定
    bool MoveX;
    
    // --プレイヤー取得時--
    // タッチ判定
    float TouchStart; // タッチを開始した座標
    float TouchEnd;   // 指を放した座標
    // プレイヤーが通る道
    const int RightLane = 1;
    const int LeftLane = -1;
    const float LaneWidth = 2.0f;
    // 昇る道
    int targetLane;

    // ---プレイヤーのサイズ---
    GameObject Player;
    Vector3 PlayerSize;
    // 接触したオブジェクト
    GameObject hitObj;
    // 接触したオブジェクトのRigidbody
    Rigidbody2D hitObjRig;
    // 接触した座標
    Vector3 hitPoint;
    // プレイヤーが接触した判定
    bool playerHit;

	// Use this for initialization
	void Start ()
    {
        //プレイヤーのサイズを取得
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerSize = Player.transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 通常の移動
        if (!playerHit)
        {
            // UFOが画面外に行かないように制限
            if (transform.position.x >= 2)
                MoveX = false;
            if (transform.position.x <= -2)
                MoveX = true;

            // 左右移動
            if (MoveX)
                ufoPos.x = transform.position.x + Mathf.Sin(Time.deltaTime * speed) * 0.5f;
            else
                ufoPos.x = transform.position.x - Mathf.Sin(Time.deltaTime * speed) * 0.5f;

            // 上下移動
            ufoPos.y = Mathf.Cos(Time.time * speed) * 0.04f;
            // 移動実行
            transform.position = ufoPos;
        }
        else
        {
            // マウスクリックまたはタップを開始したとき
            if (Input.GetMouseButtonDown(0))
            {
                TouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            }
            // マウス、タップを離したとき
            if (Input.GetMouseButtonUp(0))
            {
                TouchEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

                // 離した位置がタップ開始位置より右の場合
                if (TouchEnd > TouchStart)
                {
                    MoveRight();
                }
                // 離した位置がタップ開始位置よりも左の場合
                if (TouchEnd < TouchStart)
                {
                    MoveLeft();
                }
            }

            // 昇るレーンを変更
            float ratioX = targetLane * LaneWidth;

            // 移動実行
            ufoMove = new Vector3(ratioX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, ufoMove, 3.0f * Time.deltaTime);
            transform.position = ufoPos;
        }
	}

    void MoveRight()
    {
        if (targetLane < RightLane)
        {
            targetLane++;
        }
    }

    void MoveLeft()
    {
        if (targetLane > LeftLane)
        {
            targetLane--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //接触したオブジェクトを取得
            hitObj = collision.gameObject;

            // 接触した地点の座標を取得
            foreach (ContactPoint2D contact in collision.contacts)
            {
                hitPoint = contact.point;
            }

            // オブジェクトが下から接触した場合のみ処理を実行
            if (hitPoint.y > hitObj.transform.position.y)
            {
                UFO_Action(hitObj);
            }
            else
                return;
            
        }
    }

    void UFO_Action(GameObject hitObj)
    {
        hitObj.transform.parent = transform;

        // hitObjを自身の位置まで移動しつつサイズを小さくしていく
        hitObj.transform.position = Vector3.Lerp(hitObj.transform.position, transform.position, 1.0f * Time.deltaTime);
        hitObj.transform.localScale = Vector3.Lerp(PlayerSize, Vector3.zero, 1.0f * Time.deltaTime);

        // hitObjのRigidbodyを取得し重力を無視する
        hitObjRig = hitObj.GetComponent<Rigidbody2D>();
        hitObjRig.gravityScale = 0;

        playerHit = true;
    }
}
