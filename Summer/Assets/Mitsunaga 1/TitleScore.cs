using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScore : MonoBehaviour
{
    public Image scoreImage00;
    public Image scoreImage0;
    public Image scoreImage;

    public Sprite[] numImage;
    public List<int> number = new List<int>();

    // Use this for initialization
    void Start()
    {
        // 宣言する場所
        int numberSize = 30;
        RectTransform scoreRT = GetComponent<RectTransform>();
        int score = PlayerPrefs.GetInt("HighScore");
        int digit = score;

        // 1桁ごとにnumberリストに入れていく
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

        // スコアを中心に表示するために桁数分Positionをずらす
        scoreRT.position += new Vector3((numberSize / 2) * (number.Count - 3), 0.0f, 0.0f);

        // 既にあるScoreImageに数字に合った画像を入れる
        scoreImage00.sprite = numImage[number[0]];
        scoreImage0.sprite = numImage[number[1]];
        scoreImage.sprite = numImage[number[2]];

        // 大きい桁のScoreImageを複製して、数字にあった画像をいれる
        for (int i = 1; i < number.Count - 2; i++)
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

    void Update()
    {
        // Oボタンでハイスコアをリセット
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            SceneManager.LoadScene("TitleScene");
        }
    }
}