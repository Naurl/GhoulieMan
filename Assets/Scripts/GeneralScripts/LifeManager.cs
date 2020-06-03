using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {


    public int startingLives;
    private int lifeCounter;
    private Text lifeText;

    private GameObject player;
    public GameObject gameOverScreen;

    // Use this for initialization
	void Start () {
        lifeText = this.GetComponent<Text>();
        lifeCounter = startingLives;
        player = GameManager.instance.Player;
    }
	
	// Update is called once per frame
	void Update () {
        lifeText.text = "x " + lifeCounter;

        if(lifeCounter <= 0)
        {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }
	}

    public void Givelife()
    {
        lifeCounter++;

    }

    public void TakeLife()
    {
        lifeCounter--;
    }
}
