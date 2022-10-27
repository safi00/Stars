using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, ICollectable
{
    public static event Action OnCoinCollectable;
    public void collect()
    {
        Debug.Log("COIN");
        Destroy(gameObject);
        OnCoinCollectable?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
