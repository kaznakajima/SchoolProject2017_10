using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // プレイヤーのRigidbody
    public Rigidbody2D PlayerRig;

    // 移動先の位置
    Vector3 movePos;

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

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
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
            if(TouchEnd > TouchStart)
            {
                PlayerRen.sprite = PlayerSp[2];
                MoveRight();
            }
            // 離した位置がタップ開始位置よりも左の場合
            if(TouchEnd < TouchStart)
            {
                PlayerRen.sprite = PlayerSp[3];
                MoveLeft();
            }
        }

        // 昇るレーンを変更
        float ratioX = targetLane * LaneWidth;

        // 移動実行
        movePos = new Vector3(ratioX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, movePos, 3.0f * Time.deltaTime);
    }

    void MoveRight()
    {
        if(targetLane < RightLane)
        {
            targetLane++;
        }
    }

    void MoveLeft()
    {
        if(targetLane > LeftLane)
        {
            targetLane--;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Block")
        {
            PlayerRen.sprite = PlayerSp[1];

            //Playeranim.SetTrigger("JumpNomal");

            PlayerRig.velocity = transform.up * 7.0f;

            gameObject.layer = 8;

            Destroy(c.gameObject);

            StartCoroutine(LayerChange());
        }
    }

    IEnumerator LayerChange()
    {
<<<<<<< HEAD
        yield return new WaitForSeconds(0.75f);
=======
        yield return new WaitForSeconds(0.7f);
>>>>>>> Nakajima

        PlayerRen.sprite = PlayerSp[0];

        gameObject.layer = 0;
    }
}
