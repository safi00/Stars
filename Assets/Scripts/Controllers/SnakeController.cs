using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{
    // Settings / Stats
    [SerializeField] public float MoveSpeed = 5;
    [SerializeField] public float BodySpeed = 5;
    [SerializeField] public float SteerSpeed = 180;
    [SerializeField] public int   Gap = 85;
    [SerializeField] public static float PlayerHealth;

    // prefab bodies References
    [SerializeField] public GameObject Head;
    [SerializeField] public GameObject Tail;
    [SerializeField] public GameObject[] BodyPrefabs;
    [SerializeField] public GameObject bodies;
    [SerializeField] public GameObject powerups;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 3;
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GrowSnake();
        }
        if (Input.GetKey(KeyCode.F))
        {
            ReadjustHead();
            GrowSnake();
        }
        if (Input.GetKey(KeyCode.X))
        {
            GainHearts();
            Debug.Log("huh");
        }
        CheckBody();
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

    private void GrowSnake()
    {
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
    private void Deletebody()
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            Destroy(BodyParts[i]);
        }
        BodyParts.Clear();
    }

    private void CheckBody()
    {
        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }
    }
    private void ReadjustHead()
    {
        transform.GetChild(0).transform.position = transform.position;
    }      

    private void GainHearts()
    {
        PlayerHealth += 1;
        Debug.Log(PlayerHealth);
        ICollectable col = powerups.GetComponent<ICollectable>();
        if (col != null)
        {
            Debug.Log("HEART");
            col.Collect("HEART");
        }
    }

    private void OnEnable()
    {
        CoinController.OnCoinCollectable += GrowSnake;
    }
    private void OnDisable()
    {
        CoinController.OnCoinCollectable -= GrowSnake;
    }

}
