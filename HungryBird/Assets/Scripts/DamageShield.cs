using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShield : Shield
{
    protected override void Start()
    {
        visualBarTag = "DamageShieldBar";
        base.Start();
    }
}
