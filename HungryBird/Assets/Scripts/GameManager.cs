using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource backGroundMusic;

    public Text scoreText;
    public Text eggText;
    public int playerStartEggs = 2;
    public GameObject joysticks;
    public ShowControllButtons controllTip;

    [HideInInspector]
    public List<EyesFallower> _eyeFallowersList = new List<EyesFallower>();

    bool isGameRunning = false;
    string playerName;
    bool firstGame = true;
    Transform player;
    E_FallowerType fallowerType;   //fallower has access
    int score;


    public Transform Player { get { return player; } set { player = value; SetFallowers(E_FallowerType.PLAYER); } }
    public int Score { get { return score; } set { score = value; scoreText.text = score.ToString(); } }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // fallowers fallow
        SetFallowers(E_FallowerType.CAMERA);
    }

    public void StartGame()
    {
        Cursor.visible = false;
        if (isGameRunning) return;
        isGameRunning = true;

        if (backGroundMusic.isPlaying)
            SpawnerManager.instance.Restart();
        else
            backGroundMusic.Play();

        if (firstGame) ShowControl();

        joysticks.SetActive(true);

        SpawnerManager.instance.SpawnInteractibleObjects();
        LevelManager.instance.StartLevel();
        ResetHUD();             
    }
    
    public void ShowControl()
    {
        controllTip.ControllTip();
        firstGame = false;        
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Ranking _ranking = new Ranking();
        _ranking.LoadRank();
        _ranking.AddNewScore(playerName, score, LevelManager.instance.CurrentLvl);
        MenuManager.instance.GameOver();
        LevelManager.instance.InterruptChangingLvls();
        SetFallowers(E_FallowerType.CAMERA);
        isGameRunning = false;   // prevent to klick few times Play
        StopAllCoroutines();
        joysticks.SetActive(false);
    }

    // add scripts that fallow player
    public void AddPlayerFollowers(EyesFallower _eyeFallower)
    {
        _eyeFallowersList.Add(_eyeFallower);
        _eyeFallower.SetFallowers(fallowerType);
    }

    public void RemovePlayerFallower(EyesFallower _eyeFallower)
    {
        _eyeFallowersList.Remove(_eyeFallower);
    }

    public void SetFallowers(E_FallowerType fallowerType)
    {
        this.fallowerType = fallowerType;
        foreach (EyesFallower fallower in _eyeFallowersList)
            fallower.SetFallowers(fallowerType);
    }

    public void ResetHUD()
    {
        Score = 0;
        eggText.text = playerStartEggs.ToString();
    }

    public void Restart()
    {
        Cursor.visible = false;
        ResetHUD();
        SpawnerManager.instance.Restart();
        LevelManager.instance.StartLevel();
        SetFallowers(E_FallowerType.PLAYER);
    }

    // when player whant to interupt game form pause menu clicked other button that return
    public void InteruptGame()
    {
        SetFallowers(E_FallowerType.CAMERA);
        isGameRunning = false;
        SpawnerManager.instance.InteruptGame();
        LevelManager.instance.InterruptChangingLvls();
        joysticks.SetActive(false);
    }

    public void SetPlayerName(Text _text)
    {
        playerName = _text.text;
    }
}
