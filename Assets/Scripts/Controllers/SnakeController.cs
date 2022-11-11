using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{
    [Header("Settings / Stats")]
    [SerializeField] public double MoveSpeed;
    [SerializeField] public double BodySpeed;
    [SerializeField] public float SteerSpeed;
    [SerializeField] public double Bodygap;
    [SerializeField] public static float PlayerHealth;

    [Header("prefab bodies References")]
    [SerializeField] public GameObject Head;
    [SerializeField] public GameObject Tail;
    [SerializeField] public GameObject[] BodyPrefabs;
    [SerializeField] public GameObject bodies;
    [SerializeField] public GameObject[] powerUps;
    [SerializeField] public GameObject HEADS;

    [Header("player Lists that get filled while playing")]
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    [Header("IFRAME stuff")]
    [SerializeField] public GameObject damageglow;
    [SerializeField] public float flashduration;
    [SerializeField] public bool InvulnerabilityFrames;

    [Header("MISC")]
    [SerializeField] public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("checkPlayerPos", 3.0f, 10.0f);
        ResetPlayerStats();
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        transform.position += transform.forward * ((int)MoveSpeed) * Time.deltaTime;

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        CheckBody();
    }

    /// <summary>
    /// just incase a weird bug happens and the players falls out of bounds
    /// </summary>
    private void checkPlayerPos()
    {
        Debug.Log("check");
        List<float> poslist = new List<float>
        {
            getSnakeHead().transform.position.x,
            getSnakeHead().transform.position.z
        };
        for (int i = 0; i < poslist.Count; i++)
        {
            switch (poslist[i])
            {
                case < 0:
                    WallHit();
                    returnToSpawn();
                    break;
                case > 100:
                    WallHit();
                    returnToSpawn();
                    break;
            }
        }
        if (getSnakeHead().transform.position.y > 0.5 || getSnakeHead().transform.position.y < -5)
        {
            WallHit();
            returnToSpawn();
        }
        ReadjustHead();
    }
    private void returnToSpawn()
    {
        TelePortPlayer(3, 0, 3);
        Quaternion target = Quaternion.Euler(0, 0, 0);
        transform.rotation = target;
    }
    private void ResetPlayerStats()
    {
        MoveSpeed = 5;
        BodySpeed = 5;
        SteerSpeed = 180;
        Bodygap = 10;
        PlayerHealth = 3;
        UIController.PlayerScoreMultiplier = 1;
    }
    public float getPlayerHealth()
    {
        return PlayerHealth;
    }
    public Vector3 PlayerPostion()
    {
        //player position
        float playerPosX = transform.position.x;
        float playerPosY = transform.position.y;
        float playerPosZ = transform.position.z;

        return new Vector3(playerPosX, playerPosY, playerPosZ);
    }
    public void TelePortPlayer(float posX, float posY, float posZ)
    {
        Vector3 newPos = new Vector3(posX, posY, posZ);
        transform.position = newPos;
    }

    /// <summary>
    /// adding another segment to the snake and since the body comes from the the player position i make the player iFrame for 3 frames and false so it doesnt flash and confuse the player
    /// </summary>
    private void GrowSnake()
    {

        StartCoroutine(Flashing(3, false));
        // Instantiate body instance and
        // add it to the list
        int count = BodyParts.Count;

        Deletebody();
        if (count<=0)
        {
            int randomBodyPart = Random.Range(0, 2);
            GameObject body = Instantiate(BodyPrefabs[randomBodyPart], PlayerPostion(), Quaternion.identity);
            BodyParts.Add(body);
            body.transform.parent = bodies.transform;
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                int randomBodyPart = Random.Range(0, 2);
                GameObject body = Instantiate(BodyPrefabs[randomBodyPart], PlayerPostion(), Quaternion.identity);
                BodyParts.Add(body);
                body.transform.parent = bodies.transform;
            }
        }
        AddTail();
    }
    private void AddTail()
    {
        // Instantiate tail instance and
        // add it to the list
        GameObject tail = Instantiate(Tail, PlayerPostion(), Quaternion.identity);
        BodyParts.Add(tail);
        tail.transform.parent = bodies.transform;
    }
    private GameObject getSnakeHead()
    {
        return HEADS.transform.GetChild(0).gameObject;
    }

    private void ReadjustHead()
    {
        Destroy(getSnakeHead());
        // Instantiate head instance and
        // add it to the list
        GameObject head = Instantiate(Head, PlayerPostion(), Quaternion.identity);
        head.transform.rotation = transform.rotation;
        head.transform.parent = HEADS.transform;
    }
    private void Deletebody()
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            Destroy(BodyParts[i]);
        }
        BodyParts.Clear();
    }

    /// <summary>
    /// This method gives the snake model its snakelike feature where the body trails behind and slithers
    /// done so with a positionhistory 
    /// </summary>
    private void CheckBody()
    {
        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(((int)(index * Bodygap) + 1), 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * ((int)BodySpeed) * Time.deltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }
    }
    ///the methods for the power ups ui
    public void GainHearts()
    {
        if (PlayerHealth < 10)
        {
            PlayerHealth += 1;
            eventCheck("HEART");
        }
        else
        {
            float gainedpoints = (float)(50 * UIController.PlayerScoreMultiplier);
            UIController.PlayerScore += gainedpoints;
        }
    }
    public void GainSpeed()
    {
        if (MoveSpeed < 10)
        {
            MoveSpeed += 0.5;
            BodySpeed += 0.5;
            switch (MoveSpeed)
            {
                case < 6:
                    Bodygap -= 1;
                    break;
                case 6:
                    Bodygap = 8;
                    break;
                case 7:
                    Bodygap = 7;
                    break;
                case 8:
                    Bodygap = 6;
                    break;
                case 9:
                    Bodygap = 5;
                    break;
            }
            double currentMultiplier = UIController.PlayerScoreMultiplier;
            if (currentMultiplier < 2)
            {
                UIController.PlayerScoreMultiplier += 0.1;
            }
            eventCheck("SPEED");
        }
        else
        {
            float gainedpoints = (float)(100 * UIController.PlayerScoreMultiplier);
            UIController.PlayerScore += gainedpoints;
        }
    }
    public void gainPoints()
    {
        float gainedpoints = (float)(200 * UIController.PlayerScoreMultiplier);
        UIController.PlayerScore += gainedpoints;
        eventCheck("POINT");
    }
    /// <summary>
    /// The diffrence between wall hit and hurtsnake is that wall hit is supposed to push you back
    /// So the player can recover and not get stuck in walls
    /// </summary>
    private void WallHit()
    {
        Vector3 sixtyBack;
        switch (PositionsHistory.Count)
        {
            case < 60:
                sixtyBack = PositionsHistory[PositionsHistory.Count - 1];
                TelePortPlayer(sixtyBack.x, sixtyBack.y, sixtyBack.z);
                break;
            case > 60:
                sixtyBack = PositionsHistory[60];
                TelePortPlayer(sixtyBack.x, sixtyBack.y, sixtyBack.z);
                break;
        }
        HurtSnake();
    }
    private void HurtSnake()
    {
        if (!InvulnerabilityFrames)
        {
            if (PlayerHealth >= 2)
            {
                PlayerHealth -= 1;
                ReadjustHead();
                StartCoroutine(Flashing(10, true));

            }
            else
            {
                Loader.Load(Loader.Scene.GameOver);
            }
        }
    } 

    /// <summary>
    /// This methodis used to indicate the IFrames (InvulnerabilityFrames)
    /// So the player can make a mistake and then recover without having to worry about taking damage again
    /// </summary>
    private IEnumerator Flashing(float numberOfFlashes, bool flashes)
    {
        int temp = 0;
        InvulnerabilityFrames = true;
        if (flashes)
        {
            playHurtSound();
        }
        while (temp < numberOfFlashes)
        {
            if (flashes) 
            {
                damageglow.GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(flashduration); 
            if (flashes)
            {
                damageglow.GetComponent<MeshRenderer>().enabled = false;
            }
            yield return new WaitForSeconds(flashduration);
            temp++;
        }
        InvulnerabilityFrames = false;
    }
    // event command for the power ups and have them pull straight from the powerup list via the index. 0 = heart, 1 = speed & 2 = points.
    private void eventCheck(String eventName)
    {
        if (eventName == "HEART")
        {
            IEvent events = powerUps[0].GetComponent<IEvent>();
            if (events != null)
            {
                Debug.Log("POWER UP");
                events.playEvent(eventName);
            }
        }
        if (eventName == "SPEED")
        {
            IEvent events = powerUps[1].GetComponent<IEvent>();
            if (events != null)
            {
                Debug.Log("POWER UP");
                events.playEvent(eventName);
            }
        }
        if (eventName == "POINT")
        {
            IEvent events = powerUps[2].GetComponent<IEvent>();
            if (events != null)
            {
                Debug.Log("POWER UP");
                events.playEvent(eventName);
            }
        }
    }
    private void playHurtSound()
    {
        SoundManager.Instance.playSound(clip);
    }
    /// <summary>
    /// This methods are here to subcribe to events
    /// so when a coin gets collected all the other scripts know to tun a method
    /// </summary>
    private void OnEnable()
    {
        CoinController.OnCoinCollectable += GrowSnake;
        Hurt.OnPlayerWallHitCollision += WallHit;
        Bite.OnBitePain += HurtSnake;
        PowerUPController.OnQCoinsCollectable += GrowSnake;
    }
    private void OnDisable()
    {
        CoinController.OnCoinCollectable -= GrowSnake;
        Hurt.OnPlayerWallHitCollision -= WallHit;
        Bite.OnBitePain -= HurtSnake;
        PowerUPController.OnQCoinsCollectable -= GrowSnake;
    }
}
