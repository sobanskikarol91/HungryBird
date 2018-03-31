using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    public Text scoreInRank;
    public LevelManager lvlManager;

    void OnEnable()
    {
        GetComponent<Text>().text = "Score: " + GameManager.instance.Score.ToString() + "    Lvl: " + lvlManager.CurrentLvl;
        ScoreInRank();
    }

    void ScoreInRank()
    {
        Ranking r = new Ranking();
        int scoreIndex = r.CurentScoreRank();


        if (scoreIndex == 0)
            scoreInRank.text = "Highest Score!";
        else if (scoreIndex > 9)
            scoreInRank.text = "";
        else
            scoreInRank.text = "Position in Rank: "+ (scoreIndex+1);
    }
}
