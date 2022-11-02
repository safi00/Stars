using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, ICollectable
{
    public static event Action OnCoinCollectable;
    public void Collect(string Collectable)
    {
        Debug.Log(Collectable);
        Destroy(gameObject);
        OnCoinCollectable?.Invoke();
    }

    private void movecoin() { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
