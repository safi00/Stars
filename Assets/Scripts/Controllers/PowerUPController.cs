using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPController : MonoBehaviour, ICollectable
{
    public static event Action OnPointsCollectable;
    public static event Action OnHeartsCollectable;
    public void Collect(string Collectable)
    {
        Debug.Log(Collectable);
        switch (Collectable)
        {
            case "POINT":
                Console.WriteLine("obtained " + Collectable);
                OnPointsCollectable?.Invoke();
                break;
            case "HEART":
                Console.WriteLine("obtained " + Collectable);
                OnHeartsCollectable?.Invoke();
                break;            
        }
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
