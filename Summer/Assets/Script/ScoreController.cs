using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    // プレイヤーの参照
    public GameObject Player;

    public Text scorelabel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // スコア
        float score = GetScore();

        scorelabel.text = ("Score : " + score + "m");
	}

    // スコアの計算
    float GetScore()
    {
        // プレイヤーのジャンプした距離を測る
        return Player.transform.position.y + 4;
    }
}
