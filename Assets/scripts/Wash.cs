using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wash : MonoBehaviour {

    public bool m_gazed;
    public GameObject spray;
    public float sprayAcceleration = 10f;
    public float sprayDeacceleration = 10f;

    public SpriteAnimator sprite_animator;
    bool full_spray = false;

    // Use this for initialization
    void Start () {
        SetGazedAt(false);
    }
	
	// Update is called once per frame
	void Update () {
        update_sprite();
        update_particle_emitter();
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

    void update_particle_emitter()
    {
        if (m_gazed)
        {
            if(!full_spray)
            {
                spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = spray.GetComponent<EllipsoidParticleEmitter>().maxEmission + sprayAcceleration;
                if (spray.GetComponent<EllipsoidParticleEmitter>().maxEmission >= 100)
                {
                    full_spray = true;
                    spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = 100;
                }
            }
        }
        else
        {
            if(full_spray)
            {
                full_spray = false;
            }
            spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = Math.Max(0, spray.GetComponent<EllipsoidParticleEmitter>().maxEmission - sprayDeacceleration);
        }
    }

    void update_sprite()
    {
        //if not finished, animate
        if (sprite_animator.is_finished())
            m_gazed = false;
        else if(full_spray)
            sprite_animator.next_frame();
    }
}
