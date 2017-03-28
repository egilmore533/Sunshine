using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wash : MonoBehaviour {

    public SpriteAnimator sprite_animator;
    public bool m_gazed;


    // Use this for initialization
    void Start () {
        SetGazedAt(false);
        
        ScoreManager.graffiti_count++;
    }
	
	// Update is called once per frame
	void Update () {
        update_sprite();
    }

    public void SetGazedAt(bool gazedAt)
    {
        m_gazed = gazedAt;
    }

    //These functions are the ones that are called in the editor
    public void OnGazeEnter()
    {
        this.SetGazedAt(true);
        ScoreManager.washing = m_gazed;
    }

    public void OnGazeExit()
    {
        this.SetGazedAt(false);

        ScoreManager.washing = m_gazed;
    }

    void update_sprite()
    {
        //if not finished, animate
        if (sprite_animator.is_finished())
        {
            ScoreManager.add_score(10);
            ScoreManager.graffiti_count--;
            gameObject.SetActive(false);
            ScoreManager.washing = false;
        }
            
        else if(ScoreManager.full_spray && m_gazed)
            sprite_animator.next_frame();
    }
}
