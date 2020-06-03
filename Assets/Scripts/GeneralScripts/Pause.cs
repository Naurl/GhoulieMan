using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {


    public static bool GameIsPaused;

    public GameObject audioManajer;
    private AudioSource audioSource;

    private AudioSource pauseAudio;
    public AudioClip pauseClip;

	// Use this for initialization
	void Start () {
        audioSource = audioManajer.GetComponent<AudioSource>();
        pauseAudio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
	}

    void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        audioSource.Play();

        if(pauseClip != null)
        {
            pauseAudio.PlayOneShot(pauseClip);
        }
        
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;

        audioSource.Pause();

        if (pauseClip != null)
        {
            pauseAudio.PlayOneShot(pauseClip);
        }
    }
}
