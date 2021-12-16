using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : TankState
{
    private float _moveValCount;

    public override void Move()
    {
        base.Move();
        _moveValCount += Time.fixedDeltaTime;
        if (_moveValCount >= 2f)
        {
            RandomMove();
            _moveValCount = 0;
        }
    }

    public override void Die()
    {
        UiManager.uiManager.UpdatePlayerScore();
        base.Die();
        MapCreator.mapCreator.CreateEnemy();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        string tagInfo = other.gameObject.tag;
        if (tagInfo.Equals("Barrier") || tagInfo.Equals("Enemy"))
        {
            V *= -1;
            H *= -1;
        }

        if (tagInfo.Equals("Tank"))
        {
            Attack();
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        string tagInfo = other.gameObject.tag;
        if (tagInfo.Equals("Wall"))
        {
            Attack();
        }
    }

    public void TankAI()
    {
        
    }

    public void RandomMove()
    {
        switch (Random.Range(0, 8))
        {
            case 0: case 1:
                V = 1;
                H = 0;
                break;
            case 2: case 3: 
                V = -1;
                H = 0;
                break;
            case 4: case 5:
                V = 0;
                H = -1;
                break;
            case 6: case 7:
                V = 0;
                H = 1;
                break;
        }
    }
}
