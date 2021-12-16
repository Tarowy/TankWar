using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : TankState
{
    public AudioClip idleClip;
    public AudioClip driveClip;
    public AudioClip dieClip;
    public int life = 3;

    // Start is called before the first frame update
    void Start()
    {
        _shieldState.SetActive(true);
        UiManager.uiManager.UpdateLifeValue(life);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ReduceGodTime();
        Attack();
    }

    private void FixedUpdate()
    {
        //将控制状态的代码放到物理更新里面，防止受力不均匀产生抖动
        Move();
    }

    public override void Move()
    {
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");
        if (Math.Abs(H) <= 0.05f && Math.Abs(V) <= 0.05f)
        {
            _audioSource.clip = idleClip;
        }
        else
        {
            _audioSource.clip = driveClip;
        }
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        base.Move();
    }

    public override void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            base.Attack();
        }
    }

    public override void Die()
    {
        if (_god || --life > 0)
        {
            _audioSource.Play();
            UiManager.uiManager.UpdateLifeValue(life);
            return;
        }
        UiManager.uiManager.ShowGameOver();
        _audioSource.clip = dieClip;
        base.Die();
    }
}
