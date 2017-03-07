using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static Text score_text;
    static float score;
    static float game_time;

    // Use this for initialization
    void Start()
    {
        score_text = GetComponentInChildren<Text>();
        score = 0;
        update_score();

    }

    // Update is called once per frame
    void Update()
    {
        update_game_time();
    }

    void update_game_time()
    {
        game_time = Time.time;
    }

    public static float get_game_time()
    {
        return game_time;
    }

    public static Text get_score_text()
    {
        return score_text;
    }

    static void update_score()
    {
        score_text.text = "Score: " + score;
    }

    public static void add_score(int points_to_add)
    {
        score += (1 / (game_time / 1000f)) * points_to_add;
        update_score();
    }
}
