using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // プレイヤーのRigidbody
    public Rigidbody2D PlayerRig;
    // 衝突したオブジェクトのBoxCollider2D
    public CapsuleCollider2D PlayerCollision;

    // 移動先の位置
    Vector3 movePos;
    // 接触した部分
    Vector3 point;
    // ObstacleControllerの参照
    ObstacleController _obstacleController;

    // タッチ判定
    float TouchStart; // タッチを開始した座標
    float TouchEnd;   // 指を放した座標

    // プレイヤーが通る道
    const int RightLane = 1;
    const int LeftLane = -1;
    const float LaneWidth = 2.0f;

    // 昇る道
    int targetLane;

    // プレイヤーのスプライト
    [SerializeField]
    SpriteRenderer PlayerRen;
    [SerializeField]
    Sprite[] PlayerSp;

    // スタート判定
    public static bool gameStart;
    // 左右移動判定
    bool LaneMove;
    // 衝突判定
    bool hit;

    // Use this for initialization
    void Start()
    {
        gameStart = false;
        PlayerRig.simulated = false;
        StartCoroutine(GameStart());
        hit = false;
    }

    // 3秒たったらゲームスタート
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3.5f);

        gameStart = true;

        PlayerRig.simulated = true;
    }

    // Update is called once per frame
    void Update()
    {
        // マウスクリックまたはタップを開始したとき
        if (Input.GetMouseButtonDown(0) && gameStart)
        {
            TouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        // マウス、タップを離したとき
        if (Input.GetMouseButtonUp(0) && gameStart)
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
        if (!hit)
        {
            movePos = new Vector3(ratioX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, movePos, 4.0f * Time.deltaTime);
        }
    }

    void MoveRight()
    {

        if (targetLane < RightLane)
        {
            PlayerRen.sprite = PlayerSp[2];
            targetLane++;
            LaneMove = true;
        }
    }

    void MoveLeft()
    {
        if (targetLane > LeftLane)
        {
            PlayerRen.sprite = PlayerSp[3];
            targetLane--;
            LaneMove = true;
        }

    }

    //障害物に当たったときの処理
    public void ObstacleHit()
    {
        hit = true;
        PlayerRen.sprite = PlayerSp[4];
        _obstacleController.HitAction();

        if (point.x > transform.position.x)
        {
            PlayerRig.simulated = true;
            PlayerRig.velocity = transform.right * 12.0f;
            PlayerCollision.enabled = false;
        }
        if (point.x < transform.position.x)
        {
            PlayerRig.simulated = true;
            PlayerRig.velocity = -transform.right * 12.0f;
            PlayerCollision.enabled = false;
        }
        if (point.y > transform.position.y)
        {
            PlayerRig.simulated = true;
            PlayerRig.velocity = -transform.up * 12.0f;
            PlayerCollision.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            // 接触した障害物のスクリプトを参照する
            _obstacleController = collision.gameObject.GetComponent<ObstacleController>();

            // 衝突した地点の座標を取得
            foreach (ContactPoint2D contact in collision.contacts)
            {
                point = contact.point;
            }

            // 衝突した座標が足だったらジャンプ
            if (point.y < transform.position.y - 0.7f)
            {
                _obstacleController.EffectBorn();

                PlayerRen.sprite = PlayerSp[1];

                LaneMove = false;

                //Playeranim.SetTrigger("JumpNomal");

                PlayerRig.velocity = transform.up * 7.0f;

                gameObject.layer = 8;

                Destroy(collision.gameObject);

                StartCoroutine(LayerChange());
            }
            else
                return;


        }
        if (collision.gameObject.tag == "Obstacle")
        {
            // 接触した障害物のスクリプトを参照する
            _obstacleController = collision.gameObject.GetComponent<ObstacleController>();

            // 衝突した地点の座標を取得
            foreach (ContactPoint2D contact in collision.contacts)
            {
                point = contact.point;
            }

            // 衝突した座標が足だったらジャンプ
            if (point.y < transform.position.y - 0.75f)
            {
                PlayerRen.sprite = PlayerSp[1];

                LaneMove = false;

                //Playeranim.SetTrigger("JumpNomal");

                PlayerRig.velocity = transform.up * 9.0f;

                gameObject.layer = 8;

                Destroy(collision.gameObject);

                StartCoroutine(LayerChange());
            }
            else
            {
                ObstacleHit();
            }
        }

        if(collision.gameObject.tag == "UFO")
        {
            PlayerRen.sprite = PlayerSp[0];
            targetLane = 0;
        }
    }

    // レイヤーの変更
    IEnumerator LayerChange()
    {

        yield return new WaitForSeconds(0.8f);

        gameObject.layer = 9;

        if (LaneMove) { yield break; }

        PlayerRen.sprite = PlayerSp[0];

    }
}
