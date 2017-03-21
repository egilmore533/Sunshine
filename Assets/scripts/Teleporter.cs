using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    GameObject player;
    GameObject[] teleporters;
	// Use this for initialization
	void Start () {
        teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeleportPlayer()
    {
        Debug.Log("Triggered!!!!");
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
}
