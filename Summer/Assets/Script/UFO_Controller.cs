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
    Vector3 ufoPos = new Vector3(0,10,0);

    Vector3 PlayerSize;

    public float speed;


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

    //プレイヤーのスクリプト
    PlayerController playerSc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraPos = Camera.main;
        PlayerSize = player.transform.localScale;
        playerSc = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        //Playerがどこにいるか取得
        UpdatePlayerPos = cameraPos.transform.position.y;


        if (!playerhit)
        {
            
        }
        else
        {
            transform.position += ufoPos * speed * Time.deltaTime;
        }

        //もしPlayerがUFOに捕まった、playerは目標の～～mより低いか？
        if (UFOinPlayerPos + 20 >= UpdatePlayerPos)
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

                        hitObj.gameObject.transform.position = Vector2.Lerp(hitObj.transform.position,
                                            transform.position, 0.00001f * Time.deltaTime);

                        playerSc.enabled = false;

                        hitObj.transform.localScale -= new Vector3(0.01f, 0.01f, 0);

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

            player.transform.parent = null;

            //rangePos.x = Mathf.Clamp(hitObj.transform.localScale.x, (float)0f, (float)0.2f);

            //rangePos.y = Mathf.Clamp(hitObj.transform.localScale.y, (float)0f, (float)0.2f);

            hitObj.transform.localScale = Vector3.Lerp(hitObj.transform.localScale, PlayerSize, 1.0f);

            playerSc.enabled = true;

           catchflag = false;
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
