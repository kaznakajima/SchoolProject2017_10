using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;

    public ScoreManager scoreMane;

    public Animator CanvasAnim;
    public FadeManager fadeManager;

    public AudioClip countDownClip;
    public AudioClip gameOverClip;

    public AudioSource hakoAudioSource;

    bool buttonFlg;
    bool gameOverFlg;
    AudioSource cameraAudioSource;

    void Start()
    {
        // カメラについたAudioSourceを取得して、カウントダウンを再生
        cameraAudioSource = mainCamera.GetComponent<AudioSource>();
        cameraAudioSource.PlayOneShot(countDownClip);
        // カウントダウンの終了時間に合わせてPlayBGMを実行
        Invoke("PlayBGM", 4.0f);
        // ゲームスタートアニメを再生
        CanvasAnim.SetBool("GameStart", true);

        gameOverFlg = true;
        buttonFlg = true;
        
        
    }

	void Update ()
    {
        Vector3 cameraVec = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        // プレイヤーがカメラの下端に到達したら、ゲームオーバーフラグを立てる
        if (player.transform.position.y < cameraVec.y)
        {
            if (gameOverFlg)
            {
                // BGMを止め、ちーんを再生
                cameraAudioSource.loop = false;
                cameraAudioSource.Stop();
                hakoAudioSource.PlayOneShot(gameOverClip);
                // ハイスコアを更新していればハイスコアを記録
                int score = (int)scoreMane.GetScore();
                if (PlayerPrefs.GetInt("HighScore") < score)
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }
                // ゲームオーバーアニメを再生
                CanvasAnim.SetBool("GameOver", true);

                gameOverFlg = false;
            }
           
        }
	}

    public void OnClickedRetryButton()
    {
        if (buttonFlg)
        {
            hakoAudioSource.Play();

            fadeManager.nextSceneName = "Proto";
            fadeManager.isFade = true;
            buttonFlg = false;
        }
    }
    public void OnClickedYameruButton()
    {
        if (buttonFlg)
        {
            hakoAudioSource.Play();

            fadeManager.nextSceneName = "TitleScene";
            fadeManager.isFade = true;
            buttonFlg = false;
        }
    }

    public void PlayBGM()
    {
        // 若干音量を下げて、BGMを再生開始
        cameraAudioSource.volume = 0.5f;
        cameraAudioSource.Play();
    }
}
