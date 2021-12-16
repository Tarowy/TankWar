using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    [Tooltip("命数")] private int lifeNumers = 3;

    [Tooltip("生命数减少后变色")] public Sprite[] colorSprite;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ReduceGodTime();
        TankAI();
    }

    public override void Die()
    {
        if (_god || --lifeNumers > 0)
        {
            return;
        }
        base.Die();
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
