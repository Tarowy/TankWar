using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource.Play();
        Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
