using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // 障害物の種類
    [SerializeField]
    obstacleType obstacle;

    // 障害物の警告
    public GameObject warning;

    // カメラの参照
    Camera _mainCamera;

    // 障害物の座標
    Vector3 ObstaclePos = Vector3.zero;
    // 障害物のスピード
    public float speed;

    // 障害物の種類
    public enum obstacleType
    {
        Leftbird,   // 鳥
        Rightbird,
        rock,   // 石
    };

	// Use this for initialization
	void Start ()
    {
        
	}

	// Update is called once per frame
	void Update ()
    {
        switch (obstacle)
        {
            case obstacleType.Leftbird:
                warning.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                ObstaclePos.x = 1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if(transform.position.x > getCameraRange_Right().x + 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
            case obstacleType.Rightbird:
                warning.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                ObstaclePos.x = -1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if (transform.position.x < getCameraRange_Left().x - 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
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
