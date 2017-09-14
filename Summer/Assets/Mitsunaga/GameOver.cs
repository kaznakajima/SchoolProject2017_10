using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;

    public Image gameOverImage;
    public GameObject RetryButton;

    int addAlpha;

	void Start ()
    {
        addAlpha = 2;
        gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, 0);
        RetryButton.SetActive(false);
    }

	void Update ()
    {
        Vector3 cameraVec = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        if (player.transform.position.y < cameraVec.y)
        {
            gameOverImage.color += new Color(0, 0, 0, addAlpha * Time.deltaTime);
            RetryButton.SetActive(true);
        }
	}

    public void OnClickedRetryButton()
    {
        SceneManager.LoadScene("Proto");
    }
}
