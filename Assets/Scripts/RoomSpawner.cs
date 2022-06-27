using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;

    // 1 TOP EXIT - BOTTOM ENTERANCE
    // 2 RIGHT - LEFT ENTERANCE
    // 3 BOTTOM - TOP ENTERANCE
    // 4 LEFT - RIGHT ENTERANCE

    private RoomStorage roomStorage;
    private int randomRoomIndex;
    private bool spawned = false;
    void Start()
    {
        Destroy(gameObject, 4f);
        roomStorage = GameObject.FindGameObjectWithTag("RoomSpawn").GetComponent<RoomStorage>();
        Invoke("RoomSpawn", 0.25f);
    }

    private void RoomSpawn()
    {
        if (spawned == true) { return; }

        if(roomStorage.roomCount < 100) { 
            RoomChooser(1, false);
        } else if(100 <= roomStorage.roomCount && roomStorage.roomCount >= 500) {
            RoomChooser(0, false);
        } else {
            RoomChooser(0, true);
        }

        spawned = true;

        roomStorage.roomCount++;
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.CompareTag("SpawnPoint") && collider.GetComponent<RoomSpawner>().spawned == true) || collider.CompareTag("SpawnRoom"))
        {
            Destroy(gameObject);
        }
    }

    void RoomChooser(int lowerRoomIndex, bool endingRoom)
    {
        if (endingRoom)
        {
            switch (openingDirection)
            {
                case 1:
                    Instantiate(roomStorage.bottomRooms[0], transform.position, roomStorage.bottomRooms[0].transform.rotation);
                    break;

                case 2:
                    Instantiate(roomStorage.leftRooms[0], transform.position, roomStorage.leftRooms[0].transform.rotation);
                    break;

                case 3:
                    Instantiate(roomStorage.topRooms[0], transform.position, roomStorage.topRooms[0].transform.rotation);
                    break;

                case 4:
                    Instantiate(roomStorage.rightRooms[0], transform.position, roomStorage.rightRooms[0].transform.rotation);
                    break;

                default: return;
            }

        } else {

            switch (openingDirection)
            {
                case 1:
                    randomRoomIndex = Random.Range(lowerRoomIndex, roomStorage.bottomRooms.Length);
                    Instantiate(roomStorage.bottomRooms[randomRoomIndex], transform.position, roomStorage.bottomRooms[randomRoomIndex].transform.rotation);
                    break;

                case 2:
                    randomRoomIndex = Random.Range(lowerRoomIndex, roomStorage.leftRooms.Length);
                    Instantiate(roomStorage.leftRooms[randomRoomIndex], transform.position, roomStorage.leftRooms[randomRoomIndex].transform.rotation);
                    break;

                case 3:
                    randomRoomIndex = Random.Range(lowerRoomIndex, roomStorage.topRooms.Length);
                    Instantiate(roomStorage.topRooms[randomRoomIndex], transform.position, roomStorage.topRooms[randomRoomIndex].transform.rotation);
                    break;

                case 4:
                    randomRoomIndex = Random.Range(lowerRoomIndex, roomStorage.rightRooms.Length);
                    Instantiate(roomStorage.rightRooms[randomRoomIndex], transform.position, roomStorage.rightRooms[randomRoomIndex].transform.rotation);
                    break;

                default: return;
            }
        }
    }
}