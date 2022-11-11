using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUPController : MonoBehaviour, ICollectable
{
    /// <summary>
    /// essentials for powerUps
    /// </summary>
    public static event Action OnQCoinsCollectable;
    public GameObject spawnLocationsObjects;
    [SerializeField] public AudioClip clip;
    public List<Vector3> spawnLocations = new List<Vector3>();

    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public void Collect(string Collectable)
    {
        Debug.Log(Collectable);
        switch (Collectable)
        {
            case "QCOIN":
                Console.WriteLine("obtained " + Collectable);
                moveQCoin(gameObject);
                playcoinSound();
                OnQCoinsCollectable?.Invoke();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fillInVectorList();
    }

    private void playcoinSound()
    {
        SoundManager.Instance.playSound(clip);
    }
    private void moveQCoin(GameObject coin)
    {
        int randomIndex = Random.Range(0, 40);
        coin.transform.position = spawnLocations[randomIndex];
    }
    /// <summary>
    /// This methods makes a vector list, so the method above can pick at random to move the coin to, rather that despawning and spawn a new one.
    /// </summary>
    private void fillInVectorList()
    {
        for (int i = 0; i < 40; i++)
        {
            float posX = spawnLocationsObjects.transform.GetChild(i).gameObject.transform.position.x;
            float posY = 0.5f;
            float posZ = spawnLocationsObjects.transform.GetChild(i).gameObject.transform.position.z;
            Vector3 spawnLocForList = new(posX, posY, posZ);
            spawnLocations.Add(spawnLocForList);
        }
    }
}
