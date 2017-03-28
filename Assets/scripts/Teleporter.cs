using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    GameObject player;
    GameObject[] teleporters;

    float wait_time = 1.0f;
    float teleport_time;
    bool gazed = false;


	// Use this for initialization
	void Start () {
        teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
	}
	
	// Update is called once per frame
	void Update () {
        if (gazed && ScoreManager.get_game_time() >= teleport_time)
        {
            TeleportPlayer();
            gazed = false;
        }

    }

    public void TeleportPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(this.gameObject.transform.position.x, 
                                                GameObject.FindGameObjectWithTag("Player").transform.position.y, 
                                                this.gameObject.transform.position.z);

        this.gameObject.SetActive(false);

        foreach (GameObject tp in teleporters)
        {
            if (tp != this.gameObject)
            {
                tp.gameObject.SetActive(true);
            }
        }
    }

    public void PlayerGazeEnter()
    {
        teleport_time = ScoreManager.get_game_time() + wait_time;
        gazed = true;
    }

    public void PlayerGazeExit()
    {
        gazed = false;
    }
}
