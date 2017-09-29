using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // 障害物の種類
    [SerializeField]
    obstacleType obstacle;

    // StageManagerの参照
    StageManager _stageManager;

    // カメラの参照
    Camera _mainCamera;

    // 障害物の座標
    Vector3 ObstaclePos = Vector3.zero;
    // 障害物のスピード
    public float speed;
    // 左右移動の判断
    bool x_move;

    // 衝突エフェクト
    [SerializeField]
    SpriteRenderer ObstacleRen;
    [SerializeField]
    Sprite HitEffect;
    [SerializeField]
    GameObject Effect;
    // アラート警告
    [SerializeField]
    GameObject Warning;
    bool warning;


    // 障害物の種類
    public enum obstacleType
    {
        // 無害
        Nomal,
        // 雲
        Cloud,
        // 鳥
        Leftbird,
        Rightbird,
        // 飛行機
        RightPlane,
        LeftPlane,
        // 星
        Star

    };

    // Use this for initialization
    void Start()
    {

        _mainCamera = Camera.main;

        switch (obstacle)
        {
            case obstacleType.Cloud:
                if (transform.position.x <= 0)
                    x_move = true;
                else if (transform.position.x > 0)
                    x_move = false;
                break;
        }

        warning = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (obstacle)
        {
            case obstacleType.Cloud:
                if(_mainCamera.transform.position.y >= 100)
                {
                    StageMove();
                }
                break;
            case obstacleType.Leftbird:
                ObstaclePos.x = 1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x > getCameraRange().x + 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.Rightbird:
                ObstaclePos.x = -1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x < -getCameraRange().x - 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.LeftPlane:
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                ObstaclePos.x = 1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x > getCameraRange().x + 1.0f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.RightPlane:
                ObstaclePos.x = -1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x < -getCameraRange().x - 1.0f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.Star:
                ObstaclePos.y = -2;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                Vector3 distance = transform.position - _mainCamera.transform.position;
                if (distance.y < 10.0f && !warning)
                {
                    Warning.transform.position = new Vector3(transform.position.x, _mainCamera.transform.position.y + 4.0f, -1);
                    Instantiate(Warning);
                    warning = true;
                }
                if (transform.position.y < getCameraRange().y - 1.0f)
                    Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void EffectBorn()
    {
        Effect.transform.position = transform.position;
        Instantiate(Effect);
    }

    /// <summary>
    /// 足場の移動
    /// </summary>
    void StageMove()
    {
        if (x_move)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x >= 2)
                x_move = false;
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x <= -2)
                x_move = true;
        }
    }

    public void HitAction()
    {
        ObstacleRen.sprite = HitEffect;
        speed = 0;
        Destroy(gameObject, 0.25f);
    }

    /// <summary>
    /// カメラ範囲の取得
    /// </summary>
    /// <returns></returns>
    private Vector3 getCameraRange()
    {
        Vector3 CameraRange = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        CameraRange.z = 0;
        return CameraRange;
    }
}
