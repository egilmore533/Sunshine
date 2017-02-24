using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartButton : MonoBehaviour
{
    public bool m_gazed;
    public float gazeTimer = 0.0f;

    // Use this for initialization
    void Start () {
        SetGazedAt(false);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(m_gazed);
     
        if (m_gazed)
        {
            gazeTimer += Time.deltaTime;
        }
        if(gazeTimer >= 1.0f)
        {
            Application.LoadLevel(0);
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        m_gazed = gazedAt;
    }

    //These functions are the ones that are called in the editor
    public void OnGazeEnter()
    {
        SetGazedAt(true);
    }

    public void OnGazeExit()
    {
        gazeTimer = 0.0f;
        SetGazedAt(false);
    }

    public void OnGazeTrigger()
    {
       // TeleportThePlayer();
    }
}
