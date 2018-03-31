using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedShield : Shield
{
    protected override void Start()
    {
        visualBarTag = "ProtectedShieldBar";
        base.Start();
    }
}
