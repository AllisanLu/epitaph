using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
     protected override void Start() {
        base.Start();
        _health.maxValue = 100;
    }

}
