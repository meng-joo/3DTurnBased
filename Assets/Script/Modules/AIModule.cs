using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModule : MonoBehaviour
{
    private MainModule player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<MainModule>();

        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        transform.LookAt(playerPos);
    }

    void Update()
    {
        
    }
}
