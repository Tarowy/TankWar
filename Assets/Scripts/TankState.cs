using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public abstract class TankState : MonoBehaviour,Tank
{
    [Tooltip("子弹预制体")] public GameObject bulletPrefab;
    [Tooltip("子弹CD")] public float fireTimeVal = 0.4f;
    [Tooltip("CD时间累加")] protected float _timeCount;
    [Tooltip("子弹旋转角度")] protected Vector3 _bulletAngler = new Vector3(0, 0, 0);

    [Tooltip("根据方向切换图片")] public Sprite[] tankSprites; //上 右 下 左
    [Tooltip("图片渲染组件")] protected SpriteRenderer _spriteRenderer;

    [Tooltip("水平移动轴")] protected float H;
    [Tooltip("垂直移动轴")] protected float V = -1;
    [Tooltip("移动速度")] public float speed = 3f;
    
    [Tooltip("爆炸特效")] public  GameObject explosionState;
    
    [Tooltip("无敌时间间隔")] protected float _godTimeVal = 1f;
    [Tooltip("无敌模式")] public bool _god = true;
    [Tooltip("无敌护盾")] protected GameObject _shieldState;

    [Tooltip("坦克声音源")] public AudioSource _audioSource;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _shieldState = transform.GetChild(0).gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(gameObject.name+"已加载");
    }
    
    public virtual void Update()
    {
        _timeCount += Time.deltaTime;
        if (_timeCount > 1000)
        {
            _timeCount = 0.4f;
        }
    }

    /**
     * 坦克通用死法，如需更改可重写
     */
    public virtual void Die()
    {
        if (!_god)
        {
            
            //产生爆炸特效
            Instantiate(explosionState, transform.position, transform.rotation);
            //死亡
            Destroy(this.gameObject);
        }
    }

    /**
     * 移动方式可以根据坦克的不同而不同
     */
    public virtual void Move()
    {
        if (H != 0)
        {
            transform.Translate(Vector3.right*H*speed*Time.fixedDeltaTime,Space.World);
            if (H > 0)
            {
                _spriteRenderer.sprite = tankSprites[1];
                _bulletAngler = new Vector3(0, 0, -90);
            }else if (H < 0)
            {
                _spriteRenderer.sprite = tankSprites[3];
                _bulletAngler = new Vector3(0, 0, 90);
            }
            return;
        }
        
        transform.Translate(Vector3.up*V*speed*Time.fixedDeltaTime,Space.World);
        if (V > 0)
        {
            _spriteRenderer.sprite = tankSprites[0];
            _bulletAngler = new Vector3(0, 0, 0);
        }else if (V < 0) 
        {
            _spriteRenderer.sprite = tankSprites[2];
            _bulletAngler = new Vector3(0, 0, 180);
        }
    }

    /**
     * 攻击方式可以根据坦克的不同而不同
     */
    public virtual void Attack()
    {
        if (_timeCount >= fireTimeVal)
        {
            //直接改变子弹的四元数
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+_bulletAngler));
            _timeCount = 0;
        }
    }

    /**
     * 子类初始化无敌护盾后，调用该方法使护盾递减
     * 坦克通用护盾递减，如需更改可重写
     */
    public virtual void ReduceGodTime()
    {
        //无敌时间递减
        if (_god)
        {
            _godTimeVal -= Time.deltaTime;
            if (_godTimeVal <= 0)
            {
                Debug.Log(gameObject.name+"无敌时间消失");
                _shieldState.SetActive(false);
                _god = false;
            }
        }
    }
}
