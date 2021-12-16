using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayer = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        BulletMove();
    }

    protected void BulletMove()
    {
        transform.Translate(0,speed*Time.fixedDeltaTime,0,Space.Self);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Water":
                break;
            case "Heart":
                other.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayer)
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            case "Tank":
                if (!isPlayer)
                {
                    other.SendMessage("Die");
                }
                break;
        }
    }
}
