using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wash : MonoBehaviour {

    public bool m_gazed;
    public GameObject spray;
    public float sprayAcceleration = 10f;
    public float sprayDeacceleration = 10f;

    // Use this for initialization
    void Start () {
        SetGazedAt(false);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(m_gazed);
        if (m_gazed)
        {
            spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = Math.Min(100, spray.GetComponent<EllipsoidParticleEmitter>().maxEmission + sprayAcceleration);
            Debug.Log(spray.GetComponent<EllipsoidParticleEmitter>().maxEmission);

        }
        else
        {
            spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = Math.Max(0, spray.GetComponent<EllipsoidParticleEmitter>().maxEmission - sprayDeacceleration);
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
        SetGazedAt(false);
    }

    public void OnGazeTrigger()
    {
       // TeleportThePlayer();
    }
}
