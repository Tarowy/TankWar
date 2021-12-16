using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Transform startChooser;
    public Transform[] pos;

    public int _current;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        startChooser.position = pos[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        ChoseScene();
    }

    public void ChoseScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("isEnterGaming",true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_current == 0)
            {
                _current = pos.Length - 1;
                startChooser.position = pos[_current].position;
                return;
            }
            startChooser.position = pos[--_current].position;
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_current == pos.Length-1)
            {
                _current = 0;
                startChooser.position = pos[_current].position;
                return;
            }
            startChooser.position = pos[++_current].position;
        }
    }
}
