using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{

    // Settings
    public float MoveSpeed = 5;
    public float BodySpeed = 5;
    public float SteerSpeed = 180;
    public int Gap = 85;

    // References
    public GameObject[] BodyPrefabs;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    // References
    public GameObject Tail;

    // Start is called before the first frame update
    void Start()
    {
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

    private void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            GrowSnake();
        }
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
            GameObject body = Instantiate(BodyPrefabs[randomBodyPart]);
            BodyParts.Add(body);
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                int randomBodyPart = Random.Range(0, 2);
                GameObject body = Instantiate(BodyPrefabs[randomBodyPart]);
                BodyParts.Add(body);
            }
        }
        addTail();
    }
    private void addTail()
    {
        // Instantiate tail instance and
        // add it to the list
        GameObject tail = Instantiate(Tail);
        BodyParts.Add(tail);
    }
    private void Deletebody()
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            Destroy(BodyParts[i]);
        }
        BodyParts.Clear();
    }

}
