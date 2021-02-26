using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float speed = 10f;

    private float playerLowestY;

  
    void Start()
    {
        playerLowestY = player.position.y;
    }

    
    void Update()
    {
        if (player.position.y < playerLowestY)
            playerLowestY = player.position.y;

        transform.position =Vector3.Lerp(transform.position, new Vector3(player.position.x + offSet.x, playerLowestY + offSet.y, player.position.z + offSet.z), speed* Time.deltaTime);
    }
}
