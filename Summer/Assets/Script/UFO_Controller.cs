using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Controller : MonoBehaviour
{
    // Rayの長さ
    public float MaxRay;

    /// <summary>
    /// rayに触れたGameObject
    /// </summary>
    GameObject hitObj;

    /// <summary>
    /// PlayerのObject
    /// </summary>
    GameObject player;

    private Rigidbody2D rigidbody2d;


    /// <summary>
    /// UFOのposition
    /// </summary>
    Vector3 ufoOrigin;
    Vector3 ufoPos = new Vector3(0,10,0);
    Vector3 ufoMove;

    Vector3 PlayerSize;

    public float speed;
    public float ufoMoveX;


    /// <summary>
    /// カメラのPosition
    /// </summary>
    Camera cameraPos;


    /// <summary>
    /// ufoに触れたときの位置
    /// </summary>
    float UFOinPlayerPos;

    /// <summary>
    /// 常に更新し続ける位置
    /// </summary>
    float UpdatePlayerPos;

    /// <summary>
    /// ufoが運んだか判定
    /// </summary>
    bool catchflag = true;
    bool playerhit;
    // 左右移動判定
    bool MoveX;

    // タッチ判定
    float TouchStart; // タッチを開始した座標
    float TouchEnd;   // 指を放した座標

    // プレイヤーが通る道
    const int RightLane = 1;
    const int LeftLane = -1;
    const float LaneWidth = 2.0f;

    // 昇る道
    int targetLane;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraPos = Camera.main;
        PlayerSize = player.transform.localScale;
        ufoOrigin = transform.position;
    }

    void Update()
    {
        //Playerがどこにいるか取得
        UFOinPlayerPos = cameraPos.transform.position.y;
        UpdatePlayerPos = cameraPos.transform.position.y;

        if (!playerhit)
        {

            if (transform.position.x >= 2)
                MoveX = false;
            else if (transform.position.x <= -2)
                MoveX = true;

            if (MoveX)
                ufoOrigin.x = transform.position.x + Mathf.Sin(Time.deltaTime * speed) * 0.5f;
            else
                ufoOrigin.x = transform.position.x - Mathf.Sin(Time.deltaTime * speed) * 0.5f;

            ufoOrigin.y += Mathf.Cos(Time.time * speed) * 0.04f;
            transform.position = ufoOrigin;
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

            ufoMove = new Vector3(ratioX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, ufoMove, 3.0f * Time.deltaTime);
            transform.position += ufoPos * speed * Time.deltaTime;
        }

        //もしPlayerがUFOに捕まった、playerは目標の～～mより低いか？
        if (UFOinPlayerPos + 50 >= UpdatePlayerPos)
        {

            if (catchflag)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, -MaxRay);

                Debug.DrawRay(transform.position, transform.up * -MaxRay, Color.red);

                if (hit.collider)
                {


                    if (hit.collider.tag == "Player")
                    {

                        hitObj = hit.collider.gameObject;
                        hitObj.layer = 8;

                        rigidbody2d = hitObj.GetComponent<Rigidbody2D>();
                        rigidbody2d.gravityScale = 0;
                        rigidbody2d.simulated = false;

                        hitObj.transform.localScale = Vector3.Lerp(PlayerSize, Vector3.zero, 1.0f);

                        //hitObj.gameObject.transform.position = Vector2.Lerp(hitObj.transform.position,
                        //                    transform.position, 1.0f * Time.deltaTime);

                        hitObj.transform.position = transform.position;

                        player.transform.parent = this.transform;

                        playerhit = true;

                    }
                }
            }
        }
        else
        {

            hitObj.layer = 9;

            rigidbody2d = hitObj.GetComponent<Rigidbody2D>();
            rigidbody2d.gravityScale = 1;
            rigidbody2d.simulated = true;

            hitObj.transform.localScale = Vector3.Lerp(hitObj.transform.localScale, PlayerSize, 1.0f);

            player.transform.parent = null;

           catchflag = false;
        }


    }

    // 右移動
    void MoveRight()
    {

        if (targetLane < RightLane)
        {

            targetLane++;
        }
    }

    // 左移動
    void MoveLeft()
    {
        if (targetLane > LeftLane)
        {
            
            targetLane--;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
        }
    }
}
