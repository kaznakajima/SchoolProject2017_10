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

    void Start()
    {
        CanvasAnim.SetBool("GameStart", true);
    }

	void Update ()
    {
        Vector3 cameraVec = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

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
        SceneManager.LoadScene("Proto");
    }
    public void OnClickedYameruButton()
    {
        SceneManager.LoadScene("TItleScene");
    }
}
