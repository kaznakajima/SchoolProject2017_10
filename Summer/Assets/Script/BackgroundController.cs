using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // ゲームの背景のスプライト
    public SpriteRenderer background;

    // 背景画像の配列
    public Sprite[] backgrounds;

    // スコアの参照
    ScoreController _score;

	// Use this for initialization
	void Start ()
    {
        _score = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScoreController>();
        switch (_score.score)
        {
            case 0:
                background.sprite = backgrounds[0];
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
