using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static TextMesh score_text;
    public static TextMesh timer;
    public static TextMesh graffiti;


    //spinning circle while washing
    public GameObject spinning_circle;
    Quaternion circle_rotation;
    public static bool washing;

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
        circle_rotation = spinning_circle.transform.rotation;

        score = 0;
        update_score();
        washing = false;

    }

    // Update is called once per frame
    void Update()
    {
        //static rotation
        transform.rotation = rotation;

        update_game_time();

        graffiti.text = "Graffiti: " + graffiti_count;

        UpdateWashCircle();
    }

    void UpdateWashCircle()
    {
        spinning_circle.gameObject.SetActive(washing);
        spinning_circle.transform.Rotate(new Vector3(0,1,0), game_time);
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
}
