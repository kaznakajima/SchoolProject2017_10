using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // ゲームの背景のスプライト
    public SpriteRenderer background;

    // 背景画像の配列
    public Sprite[] backgrounds;

    // カメラの位置
    Camera _mainCamera;

	// Use this for initialization
	void Start ()
    {
        _mainCamera = Camera.main;
        if(_mainCamera.transform.position.y >= 0)
        {
            background.sprite = backgrounds[1];
        }
        if(_mainCamera.transform.position.y > 100)
        {
            background.sprite = backgrounds[2];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
