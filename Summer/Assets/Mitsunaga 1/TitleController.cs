using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public FadeManager fadeManager;

    AudioSource titleAudioSource;

	// Use this for initialization
	void Start ()
    {
        titleAudioSource = GetComponent<AudioSource>();
	}

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount != 0||Input.GetMouseButtonDown(0))
        {
            TouchStart();
        }
	}
    void TouchStart()
    {
        titleAudioSource.Play();
        fadeManager.nextSceneName = "Proto";
        fadeManager.isFade = true;
    }
}
