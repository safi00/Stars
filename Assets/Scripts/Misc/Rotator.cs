using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Settings
    [SerializeField] public double SpinningSpeed;

    private void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, ((float)(0.1 * SpinningSpeed)), 0, Space.World);
    }
}
