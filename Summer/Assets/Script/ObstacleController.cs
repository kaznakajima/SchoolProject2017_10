using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // 障害物の種類
    [SerializeField]
    obstacleType obstacle;

    // カメラの参照
    Camera _mainCamera;

    // 障害物の座標
    Vector3 ObstaclePos = Vector3.zero;
    // 障害物のスピード
    public float speed;

    // PlayerControllerの参照
    PlayerController _playerController;

    // 衝突したオブジェクトのRigidbody
    private Rigidbody2D PlayerRig;
    // 衝突したオブジェクトのBoxCollider2D
    private BoxCollider2D PlayerCollision;
    // 衝突エフェクト
    [SerializeField]
    SpriteRenderer ObstacleRen;
    [SerializeField]
    Sprite HitEffect;


    // 障害物の種類
    public enum obstacleType
    {
        // 鳥
        Leftbird,   
        Rightbird,
        // 飛行機
        RightPlane,
        LeftPlane

    };

	// Use this for initialization
	void Start ()
    {
        // 初期位置
        switch (obstacle)
        {
            case obstacleType.Leftbird:

                break;
        };

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PlayerRig = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        PlayerCollision = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

	// Update is called once per frame
	void Update ()
    {
        switch (obstacle)
        {
            case obstacleType.Leftbird:
                ObstaclePos.x = 1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if(transform.position.x > getCameraRange_Right().x + 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.Rightbird:
                ObstaclePos.x = -1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x < getCameraRange_Left().x - 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.LeftPlane:
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                ObstaclePos.x = 2;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x > getCameraRange_Right().x + 1.0f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.RightPlane:
                ObstaclePos.x = -2;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x < getCameraRange_Left().x - 1.0f)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    // 障害物にぶつかった時の処理
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameController")
        {
            //// 衝突した地点の座標を取得
            //foreach (ContactPoint2D contact in collision.contacts)
            //{
            //    point = contact.point;
            //    Debug.Log(point);
            //}

            _playerController.ObstacleHit();
            ObstacleRen.sprite = HitEffect;
            speed = 0;
            Destroy(gameObject, 0.25f);

            HitAction();
        }
    }

    void HitAction()
    {
        if (ObstaclePos.x > 0)
        {
            PlayerRig.simulated = true;
            PlayerRig.velocity = transform.right * 15.0f;
            PlayerCollision.enabled = false;
        }
        if (ObstaclePos.x < 0)
        {
            PlayerRig.simulated = true;
            PlayerRig.velocity = -transform.right * 15.0f;
            PlayerCollision.enabled = false;
        }
    }

    private Vector3 getCameraRange_Left()
    {
        _mainCamera = Camera.main;
        Vector3 LeftRange = _mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        LeftRange.Scale(new Vector3(1f, -1f, 1f));
        LeftRange.z = 0;
        return LeftRange;
    }

    private Vector3 getCameraRange_Right()
    {
        _mainCamera = Camera.main;
        Vector3 RightRange = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0.0f));
        // 上下反転させる
        RightRange.Scale(new Vector3(1f, -1f, 1f));
        RightRange.z = 0;
        return RightRange;
    }
}
