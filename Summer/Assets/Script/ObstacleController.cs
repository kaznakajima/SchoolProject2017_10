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

    // 衝突エフェクト
    [SerializeField]
    SpriteRenderer ObstacleRen;
    [SerializeField]
    Sprite HitEffect;
    // アラート警告
    [SerializeField]
    GameObject Warning;


    // 障害物の種類
    public enum obstacleType
    {
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

        // 障害物の種類によって警告の表示する位置を決める
        switch (obstacle)
        {
            case obstacleType.Star:
                Warning.transform.position = new Vector3(transform.position.x, _mainCamera.transform.position.y + 4.5f,-1);
                Instantiate(Warning);
                break;
        }

        // 初期位置
        //switch (obstacle)
        //{

        //    case obstacleType.Star:
        //        transform.position = new Vector3(Random.Range(-2, 2), _mainCamera.transform.position.y + 5.5f, -1);
        //        break;
        //    case obstacleType.Leftbird:
        //        transform.position = new Vector3(-3, Random.Range(_mainCamera.transform.position.y - 2, _mainCamera.transform.position.y + 2), -1);
        //        break;
        //    case obstacleType.LeftPlane:
        //        transform.position = new Vector3(-3, Random.Range(_mainCamera.transform.position.y - 2, _mainCamera.transform.position.y + 2), -1);
        //        break;
        //    case obstacleType.Rightbird:
        //        transform.position = new Vector3(3, Random.Range(_mainCamera.transform.position.y - 2, _mainCamera.transform.position.y + 2), -1);
        //        break;
        //    case obstacleType.RightPlane:
        //        transform.position = new Vector3(3, Random.Range(_mainCamera.transform.position.y - 2, _mainCamera.transform.position.y + 2), -1);
        //        break;
        //}
    }

    // Update is called once per frame
    void Update()
    {

        switch (obstacle)
        {
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
                if (transform.position.y < getCameraRange().y - 1.0f)
                    Destroy(gameObject);
                break;
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
