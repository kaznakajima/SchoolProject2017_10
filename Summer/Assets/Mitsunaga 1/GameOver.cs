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

    bool buttonFlg;

    void Start()
    {
        buttonFlg = true;
        CanvasAnim.SetBool("GameStart", true);
    }

	void Update ()
    {
        Vector3 cameraVec = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        // プレイヤーがカメラの下端に到達したら、ゲームオーバーフラグを立てる
        if (player.transform.position.y < cameraVec.y)
        {
            int score = (int)scoreMane.GetScore();
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            CanvasAnim.SetBool("GameOver", true);
        }
	}

    public void OnClickedRetryButton()
    {
        if (buttonFlg)
        {
            fadeManager.nextSceneName = "Proto";
            fadeManager.isFade = true;
            buttonFlg = false;
        }
    }
    public void OnClickedYameruButton()
    {
        if (buttonFlg)
        {
            fadeManager.nextSceneName = "TitleScene";
            fadeManager.isFade = true;
            buttonFlg = false;
        }
    }
}
