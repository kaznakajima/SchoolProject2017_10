﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // 障害物の種類
    [SerializeField]
    obstacleType obstacle;

    // カメラの参照
    [SerializeField]
    Camera _mainCamera;

    // 障害物の座標
    Vector3 ObstaclePos = Vector3.zero;
    // 障害物のスピード
    public float speed;

    public enum obstacleType
    {
        bird,
        rock,
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
            case obstacleType.bird:
                ObstaclePos.x = 1;
                transform.position += ObstaclePos * speed * Time.deltaTime;
                if(transform.position.x > getCameraRange_Right().x + 0.25f)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private Vector3 getCameraRange_Left()
    {
        Vector3 LeftRange = _mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        LeftRange.Scale(new Vector3(1f, -1f, 1f));
        LeftRange.z = 0;
        Debug.Log(LeftRange);
        return LeftRange;
    }

    private Vector3 getCameraRange_Right()
    {
        Vector3 RightRange = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0.0f));
        // 上下反転させる
        RightRange.Scale(new Vector3(1f, -1f, 1f));
        RightRange.z = 0;
        Debug.Log(RightRange);
        return RightRange;
    }
}
