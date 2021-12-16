using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy
{
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ReduceGodTime();
        TankAI();
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }

    protected override void OnCollisionStay2D(Collision2D other)
    {
        base.OnCollisionStay2D(other);
    }
}
