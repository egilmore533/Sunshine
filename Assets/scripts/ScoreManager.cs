﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public static TextMesh score_text;
    public static TextMesh timer;
    public static TextMesh graffiti;

    //spinning circle while washing
    public GameObject spinning_circle;
    Quaternion circle_rotation;
    public static bool washing;
    float circle_transition_rate = 0.06f;
    public float circle_current_alpha;

    //spray animation
    public GameObject spray;
    public float sprayAcceleration = 1f;
    public float sprayDeacceleration = 10f;
    public static bool full_spray = false;
    private bool just_gazed;
    //end spray animtaion

    static float score;
    static float game_time;
    public static int graffiti_count;

    public static Canvas canvas;

    //move with camera but don't rotate with player
    Quaternion rotation;
    // Use this for initialization
    void Awake()
    {
        graffiti_count = 0;
        rotation = transform.rotation;
    }

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        Transform [] children = canvas.gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.name == "Score")
            {
                score_text = child.GetComponent<TextMesh>();
            }
            else if (child.name == "Timer")
            {
                timer = child.GetComponent<TextMesh>();
            }
            else if (child.name == "Graffiti")
            {
                graffiti = child.GetComponent<TextMesh>();
            }
        }

        spinning_circle = GameObject.FindGameObjectWithTag("circle");
        circle_current_alpha = 0;
        circle_rotation = spinning_circle.transform.rotation;

        score = 0;
        update_score();
        washing = false;
        just_gazed = false;

    }

    // Update is called once per frame
    void Update()
    {
        //static rotation
        transform.rotation = rotation;

        update_game_time();

        graffiti.text = "Graffiti: " + graffiti_count;

        UpdateWashCircle();
        update_particle_emitter();

        check_stage_finished_and_switch();
    }

    void check_stage_finished_and_switch()
    {
        if (graffiti_count <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    void UpdateWashCircle()
    {
        spinning_circle.gameObject.SetActive(true);
        spinning_circle.transform.Rotate(new Vector3(0,1,0), 4);

        if(washing)
        {
            //increase alpha
            circle_current_alpha += circle_transition_rate;
            if(circle_current_alpha > 1.0f)
                circle_current_alpha = 1.0f;
        }
        else
        {
            //decrease
            circle_current_alpha -= circle_transition_rate;
            if (circle_current_alpha < 0.0f)
                circle_current_alpha = 0.0f;
        }
        spinning_circle.gameObject.GetComponent<Renderer>().material.color =
              new Color(1.0f, 1.0f, 1.0f, circle_current_alpha);
        Debug.Log(circle_current_alpha);
    }

    void update_game_time()
    {
        game_time = Time.time;
        update_timer();
    }

    public static float get_game_time()
    {
        return game_time;
    }

    public static TextMesh get_score_text()
    {
        return score_text;
    }

    static void update_score()
    {
        score_text.text = "Score: " + (int)score;
    }

    static void update_timer()
    {
        timer.text = "Timer: " + (int)game_time;
    }
    public static void add_score(int points_to_add)
    {
        score += (1 / (game_time / 1000f)) * points_to_add;
        update_score();
    }

    void update_particle_emitter()
    {
        if (washing)
        {
            just_gazed = true;
            if (!full_spray)
            {
                spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = spray.GetComponent<EllipsoidParticleEmitter>().maxEmission + sprayAcceleration;
                if (spray.GetComponent<EllipsoidParticleEmitter>().maxEmission >= 100)
                {
                    full_spray = true;
                    spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = 100;
                }
            }
        }
        else if (just_gazed)
        {
            if (full_spray)
            {
                full_spray = false;
            }
            spray.GetComponent<EllipsoidParticleEmitter>().maxEmission = Math.Max(0, spray.GetComponent<EllipsoidParticleEmitter>().maxEmission - sprayDeacceleration);
            if (spray.GetComponent<EllipsoidParticleEmitter>().maxEmission == 0)
                just_gazed = false;
        }
    }
}
