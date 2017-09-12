using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    // プレイヤーの参照
    public GameObject Player;

    public Text scorelabel;

    public float score;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        scorelabel.text = ("Score : " + (int)score + "m");
	}

    // スコアの計算
    public float GetScore()
    {
        
        // プレイヤーのジャンプした距離を測る
        return Player.transform.position.y + 4;
    }
}
