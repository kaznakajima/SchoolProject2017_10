using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // プレイヤーのRigidbody
    public Rigidbody2D PlayerRig;

    // 移動先の位置
    Vector3 movePos;

    // プレイヤーが通る道
    const int RightLane = 1;
    const int LeftLane = -1;
    const float LaneWidth = 2.0f;

    // 走る道
    int targetLane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 ScreenPos = Camera.main.ScreenToWorldPoint(mousePos);

            if (ScreenPos.x > transform.position.x)
            {
                MoveRight();
            }
            if (ScreenPos.x < transform.position.x)
            {
                MoveLeft();
            }

            // 走るレーンを変更
            float ratioX = targetLane * LaneWidth;

            // 移動実行
            movePos = new Vector3(ratioX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, movePos, 1.0f);
        }
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
            PlayerRig.velocity = transform.up * 7.0f;

            gameObject.layer = 8;

            Destroy(c.gameObject);

            StartCoroutine(LayerChange());
        }
    }

    IEnumerator LayerChange()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.layer = 0;
    }
}
