using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    // プレイヤーの参照
    public GameObject Camera;

    public int score;

    public Image scoreImage00;
    public Image scoreImage0;
    public Image scoreImage;

    public Sprite[] numImage;
    public List<int> number = new List<int>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //いままで表示されてたスコアオブジェクト削除
        var objs = GameObject.FindGameObjectsWithTag("Score");
        foreach (var obj in objs)
        {
            if (0 <= obj.name.LastIndexOf("Clone"))
            {
                Destroy(obj);
            }
        }
        score = (int)GetScore();
        int digit = score;
        number = new List<int>();
        while (digit != -1)
        {
            
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
            if (digit == 0)
            {
                while (number.Count <= 2)
                {
                    number.Add(0);
                }
                digit = -1;
            }
            
        }
        scoreImage00.sprite = numImage[number[0]];
        scoreImage0.sprite = numImage[number[1]];

        scoreImage.sprite = numImage[number[2]];
        for (int i = 1; i < number.Count-2; i++)
        {
            //複製
            RectTransform scoreimage = (RectTransform)Instantiate(scoreImage).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector2(
                scoreimage.localPosition.x - scoreimage.sizeDelta.x * i,
                scoreimage.localPosition.y);
            scoreimage.GetComponent<Image>().sprite = numImage[number[i + 2]];
        }
    }

    // スコアの計算
    public float GetScore()
    {
        // プレイヤーのジャンプした距離を測る
        return Camera.transform.position.y * 1000;
    }
}
