using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : InteractiveObject
{
    RewardController _rewardCon;
    protected override void Start()
    {
        base.Start();
        _rewardCon = GetComponent<RewardController>();          
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.tag.Equals("Egg") || collision.tag.Equals("DamageShield"))
        {
            _rewardCon.SpawnReward();
            Death();
        }
    }
}
