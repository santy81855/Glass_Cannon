﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject player;
    int current = 0;
    private float speed = 10;
    float WPradius = 1;

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current = Random.Range(0, waypoints.Length);
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

    }

    void OnTriggerEnter(Collider n)
    {
        if (n.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    void OnTriggerExit(Collider n)
    {
        if (n.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}