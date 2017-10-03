using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // ゲームの背景のスプライト
    public SpriteRenderer background;

    // 背景画像の配列
    public Sprite[] sp_backgrounds;

    // スコアの参照
    Camera _mainCamera;

	// Use this for initialization
	void Start ()
    {
        _mainCamera = Camera.main;
        if(_mainCamera.transform.position.y >= 0)
        {
            background.sprite = sp_backgrounds[0];
        }
        if(_mainCamera.transform.position.y >= 90)
        {
            background.sprite = sp_backgrounds[1];
        }
        if(_mainCamera.transform.position.y > 100)
        { 
            background.sprite = sp_backgrounds[2];
        }
        if(_mainCamera.transform.position.y >= 290)
        {
            background.sprite = sp_backgrounds[3];
        }
        if(_mainCamera.transform.position.y >= 300)
        {
            background.sprite = sp_backgrounds[4];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
