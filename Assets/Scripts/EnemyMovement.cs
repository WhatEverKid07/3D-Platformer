using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public List<Transform> wayPointsList = new List<Transform>();

    public Transform currentWaypoint;

    public int wayPointNumber = 0;

    public float speed = 3f;

    // Use this for initialization
    void Start()
    {
        currentWaypoint = wayPointsList[wayPointNumber];
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    public void moveEnemy()
    {
        float distanceToCurrent = Vector2.Distance(transform.position, currentWaypoint.position);

        Vector3 relativePos = currentWaypoint.position - transform.position;

        transform.rotation = Quaternion.LookRotation(relativePos);
        if (distanceToCurrent == 0)
        {
            if (wayPointNumber != wayPointsList.Count - 1)
            {
                wayPointNumber++;
                currentWaypoint = wayPointsList[wayPointNumber];
            }
            else
            {
                wayPointNumber = 0;
                currentWaypoint = wayPointsList[wayPointNumber];
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
    }

}

