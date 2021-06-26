using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] StartingPos;
    public GameObject[] Rooms;
    /// <summary>Rooms 
    /// index 0 - LR
    /// index 1 - LRB
    /// index 2 - LRT 
    /// index 3 - LRTB
    /// </summary>
    [Header("Level Generation")]
    int direction;
    public float moveAmount;

    float timeBtwnRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public bool stopGeneration;
    public LayerMask room;

    int downCounter;
   







    private void Start()
    {
        int randStartingPos = Random.Range(0, StartingPos.Length);
        transform.position = StartingPos[randStartingPos].position;
        Instantiate(Rooms[0], transform.position, Quaternion.identity);


        direction = Random.Range(1, 6);
        

    }
    private void Update()
    {
        if (timeBtwnRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwnRoom = startTimeBtwRoom;
        }
        else 
        {
            timeBtwnRoom -= Time.deltaTime;
        }
    }
    // this function moves the level genearation in the direction of the move variable amd spawn rooms
    private void Move()
    {
        if (direction == 1 || direction == 2) 
        {
            if (transform.position.x <maxX)
            {
                //Move right 
                downCounter = 0;
                Vector2 newpos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newpos;

                int rand = Random.Range(0, Rooms.Length);
                Instantiate(Rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                //if it reaches the right edge move down
                direction = 5;
            }
  
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                //Move LEFT 
                downCounter = 0;
                Vector2 newpos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newpos;

                int rand = Random.Range(0, Rooms.Length);
                Instantiate(Rooms[rand], transform.position, Quaternion.identity);


                direction = Random.Range(3, 6);

            }
            else
            {
                direction = 5;
            }

        }
        else if (direction == 5)
        {
            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1&& roomDetection.GetComponent<RoomType>().type !=3)
                {
                   

                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomsDestroyer();
                        Instantiate(Rooms[3], transform.position, Quaternion.identity);

                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomsDestroyer();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(Rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }

                }
                //Move Down
                Vector2 newpos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newpos;

                int rand = Random.Range(2, 4);
                Instantiate(Rooms[rand], transform.position, Quaternion.identity);


                direction = Random.Range(1, 6);
            }
            else
            {
                //Place the exit
                stopGeneration = true;
            }

        }

    }

}
