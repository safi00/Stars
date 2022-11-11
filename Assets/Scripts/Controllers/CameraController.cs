using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }
    /// <summary>
    /// distance between player and camera
    /// </summary>
    private void HandleTranslation()
    {
        var targetPos = Player.transform.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, translateSpeed * Time.deltaTime);
    }
    /// <summary>
    /// camera rotation with the player transform
    /// </summary>
    private void HandleRotation() 
    {
        var direction = Player.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
