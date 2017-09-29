using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{

    // Unity の Inspector で値を設定
    public string nextSceneName;           // 次のシーン名
    public RectTransform FadeImageLeft;    // フェードに利用する画像 左側
    public RectTransform FadeImageRight;   // フェードに利用する画像 右側

    // Startメソッドで値を設定
    public bool isFade;        // フェードスイッチ
    bool isFadeIn;      // フェードインフラグ
    bool isFadeOut;     // フェードアウトフラグ

    public float fadeTime; // フェードのアニメーション時間
    float fadeInTime;      // フェードインのカウント
    float fadeOutTime;     // フェードアウトのカウント

    Animator FadeAnim;

    void Start()
    {
        // アニメーターを取得
        FadeAnim = GetComponent<Animator>();

        // シーン開始時はフェードインから始まる
        isFade = true;     // フェードON
        isFadeIn = true;   // フェードインON
        isFadeOut = false; // フェードアウトOFF

        // カウントの初期化
        fadeInTime = 0;
        fadeOutTime = 0;

        // 最前面へ
        this.transform.SetAsLastSibling();
    }

    void Update()
    {
        if (isFade == true)
        {
            if (isFadeOut == true)
            {
                FadeOut();   // フェードアウト
            }
            if (isFadeIn == true)
            {
                FadeIn();    // フェードイン
            }
            
        }
    }

    // フェードイン
    void FadeIn()
    {
        this.transform.SetAsLastSibling();  // 最前面へ
        fadeInTime += Time.deltaTime;       // フェード時間をカウント
        FadeAnim.SetBool("FadeIn", true);

        // カウントが設定時間以上になったら
        if (fadeInTime >= fadeTime)
        {
            isFade = false;                      // フェードスイッチOFF
            isFadeIn = false;                    // フェードインフラグOFF
            isFadeOut = true;                    // フェードアウトフラグON
            this.transform.SetAsFirstSibling();  // 最背面へ
        }
    }

    // フェードアウト
    void FadeOut()
    {
        this.transform.SetAsLastSibling();  // 最前面へ
        fadeOutTime += Time.deltaTime;      // フェード時間をカウント
        FadeAnim.SetBool("FadeOut", true);

        // カウントが設定時間以上になったら
        if (fadeOutTime >= fadeTime)
        {
            isFade = false;                          // フェードスイッチOFF
            isFadeOut = false;                       // フェードアウトフラグOFF
            SceneManager.LoadScene(nextSceneName);   // 次のシーンへ
        }
    }
}
