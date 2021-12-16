using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefab;
    
    public bool _isPlayer = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank",0.8f);
        Destroy(gameObject,0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if (_isPlayer)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
            _isPlayer = false;
            return;
        }
        Instantiate(enemyPrefab[Random.Range(0,2)], transform.position, transform.rotation);
    }
}
