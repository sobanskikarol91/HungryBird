using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    public int score = 10;
    int changeToSpawnReward;
    int totalProbability;
    public Reward[] _rewards;
    FloatingTextController ftc;

    [System.Serializable]
    public class Reward
    {
        public GameObject reward;
        public int spawnProbability;
        public float scale=1;
    }

    private void Start()
    {
        ftc = GetComponent<FloatingTextController>();
        changeToSpawnReward = SpawnerManager.instance.rewardSpawnChance;
    }

    public void SpawnReward()
    {
        ftc.CreateScoreText(score);
        GameManager.instance.Score += score;
        if (_rewards.Length == 0) return;

        // chance if it is possible to spawn reward
        int chance = Random.Range(0, 100);
        if (chance > changeToSpawnReward)             return;

        // chance whitch object will spawn
        SumRewardsProbability();
        int randomChance = Random.Range(1, totalProbability+1);


        int randomIndex = FindRewardIndex(randomChance);
        
        GameObject go = Instantiate(_rewards[randomIndex].reward, transform.position, Quaternion.identity);
        go.transform.localScale *= _rewards[randomIndex].scale;
    }

    void SumRewardsProbability()
    {
        totalProbability = 0;

        foreach (Reward r in _rewards)
            totalProbability += r.spawnProbability;        
    }

    int FindRewardIndex(int chance)
    {
       int sum = 0;
        for (int i = 0; i < _rewards.Length; i++)
        {
            sum += _rewards[i].spawnProbability;
           
            if (chance <= sum && (chance > sum - _rewards[i].spawnProbability))
                return i;
        }
        Debug.LogError(sum);
        return -1;
    }    
}
