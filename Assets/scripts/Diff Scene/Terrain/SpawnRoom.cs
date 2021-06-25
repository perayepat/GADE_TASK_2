using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;

    public LevelGeneration levelGen;

    private void Update()
    {
        //make a room if no room
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        //Spawn some rooms my dude
        if (roomDetection == null && levelGen.stopGeneration == true)
        {
            int rand = Random.Range(0, levelGen.Rooms.Length);
            Instantiate(levelGen.Rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
