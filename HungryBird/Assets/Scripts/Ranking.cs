using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    //visual ranks records on screen
    public GameObject[] visualRecords;
    public bool resetOnStart = false;
    int maxRanksAmount = 10;
    List<Rank> ranks = new List<Rank>();


    public class Rank
    {
        public string name;
        public int score;
        public int lvl;

        public Rank(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public Rank(string name, int score, int lvl)
        {
            this.name = name;
            this.score = score;
            this.lvl = lvl;
        }
    }

    //===========================================================


    void Start()
    {
        LoadRank();
       if(resetOnStart) ResetRank();
    }

    public void SaveRank()
    {     
        int currentRanksAmount = Mathf.Min(maxRanksAmount, ranks.Count);

        PlayerPrefs.SetInt("currentRanksAmount", currentRanksAmount);
        for (int i = 0; i < currentRanksAmount; i++)
        {
            PlayerPrefs.SetInt("lvl" + i, ranks[i].lvl);
            PlayerPrefs.SetInt("score" + i, ranks[i].score);
            PlayerPrefs.SetString("name" + i, ranks[i].name);
        }
    }

    public void LoadRank()
    {
        ranks.Clear(); // clear
        int currentRanksAmount = PlayerPrefs.GetInt("currentRanksAmount", 0);

        for (int i = 0; i < currentRanksAmount; i++)
        {
            int lvl = PlayerPrefs.GetInt("lvl" + i, 0);
            int score = PlayerPrefs.GetInt("score" + i, 0);
            string name = PlayerPrefs.GetString("name" + i, "-");

            ranks.Add(new Rank(name, score, lvl));
        }
    }

    public void AddNewScore(string name, int score, int lvl)
    {
        Rank newScore = new Rank(name, score, lvl);
        ranks.Add(newScore);


        //sort

        ranks.Sort(delegate (Rank a, Rank b) { return -a.score.CompareTo(b.score);});
        PlayerPrefs.SetInt("currentScoreIndex", ranks.IndexOf(newScore));

        //delete last position
        if (ranks.Count > maxRanksAmount)
            ranks.RemoveAt(maxRanksAmount);

        SaveRank();
    }


    public void ShowRank()
    {
        LoadRank();

        for (int i = 0; i < ranks.Count; i++)
        {
            Text t = visualRecords[i].transform.Find("Name").GetComponent<Text>();
            t.text = ranks[i].name;

            t = visualRecords[i].transform.Find("Score").GetComponent<Text>();
            t.text = ranks[i].score.ToString();

            t = visualRecords[i].transform.Find("Lvl").GetComponent<Text>();
            t.text = ranks[i].lvl.ToString();
        }

        //set default number
        for(int i=0;  i<maxRanksAmount;i++)
        {
            Text t = visualRecords[i].transform.Find("Nr").GetComponent<Text>();
            t.text = (i+1).ToString() + ".";
        }
    }


    private void ResetRank()
    {
        int score = 900;
        for (int i = 0; i < maxRanksAmount; i++)
        {
            PlayerPrefs.SetInt("score" + i, score-i*100);
            PlayerPrefs.SetString("name" + i, "---");
            PlayerPrefs.SetInt("lvl" + i, 0);
        }
          
        ShowRank();
    }

    public int CurentScoreRank()
    {
        return PlayerPrefs.GetInt("currentScoreIndex");
    }
}
