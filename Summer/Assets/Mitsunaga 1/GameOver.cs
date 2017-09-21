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
    public GameObject[] HakoAndButton;

    int addAlpha;

	void Start ()
    {
        addAlpha = 2;
        gameOverImage.color = new Color(1, 1, 1, 0);
        for(int i = 0; i < HakoAndButton.Length; i++)
        {
            HakoAndButton[i].SetActive(false);
        }
        
    }

	void Update ()
    {
        Vector3 cameraVec = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        if (player.transform.position.y < cameraVec.y)
        {
            if(gameOverImage.color.a < 1)
            {
                gameOverImage.color += new Color(0, 0, 0, addAlpha * Time.deltaTime);

                if(gameOverImage.color.a > 1)
                {
                    gameOverImage.color = new Color(1, 1, 1, 1);
                }
            }
            for (int i = 0; i < HakoAndButton.Length; i++)
            {
                HakoAndButton[i].SetActive(true);
            }
        }
	}

    public void OnClickedRetryButton()
    {
        SceneManager.LoadScene("Proto");
    }
}
