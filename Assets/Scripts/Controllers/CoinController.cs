using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinController : MonoBehaviour, ICollectable
{
    /// <summary>
    /// Coin Essentials
    /// </summary>
    public static event Action OnCoinCollectable;
    [SerializeField] public AudioClip clip;
    public GameObject spawnLocationsObjects;
    public List<Vector3> spawnLocations = new List<Vector3>();

    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public void Collect(string Collectable)
    {
        if (Collectable == "COIN")
        {
            Debug.Log(Collectable);
            movecoin(gameObject); 
            playcoinSound();
            OnCoinCollectable?.Invoke();
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
    private void movecoin(GameObject coin)
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
            spawnLocations.Add(spawnLocationsObjects.transform.GetChild(i).gameObject.transform.position);
        }
    }
}
