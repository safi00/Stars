using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour, IEvent
{
    public static event Action OnPlayerPainfulCollision;
    public void playEvent(string eventName)
    {
        if (eventName == "HURT")
        {
            Debug.Log(eventName);
            OnPlayerPainfulCollision?.Invoke();
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
