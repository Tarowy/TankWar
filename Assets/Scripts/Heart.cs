using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite brokenSprite;
    private SpriteRenderer _spriteRenderer;
    public bool isPlayerHeart = true;

    public GameObject _explosionState;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        _spriteRenderer.sprite = brokenSprite;
        Instantiate(_explosionState, transform.position, transform.rotation);
        if (isPlayerHeart)
        {
            UiManager.uiManager.ShowGameOver();
            return;
        }
        Debug.Log("胜利了");
    }
}
