using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void eventCheck(Collider collider, String eventName) {
        IEvent events = collider.GetComponent<IEvent>();
        if (events != null)
        {
            Debug.Log(collider.gameObject);
            events.playEvent(eventName);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectable col = other.GetComponent<ICollectable>();
        if (col != null)
        {
            col.Collect("COIN");
        }
        if (other.CompareTag("Fence"))
        {
            eventCheck(other, "HURT");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            eventCheck(collision.collider,"HURT");
        }
    }
}
