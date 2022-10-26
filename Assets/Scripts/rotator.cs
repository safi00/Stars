using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Settings
    [SerializeField] public float SpinningSpeed = 1;

    private void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, SpinningSpeed, 0, Space.World);
    }
}
