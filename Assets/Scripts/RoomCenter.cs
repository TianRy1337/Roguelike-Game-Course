using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;

    public List<GameObject> enmeies = new List<GameObject>();

    public Room theRoom;
    // Start is called before the first frame update
    void Start()
    {
        if(openWhenEnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enmeies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for(int i =0;i<enmeies.Count;i++)
            {
                if(enmeies[i]==null)
                {
                    enmeies.RemoveAt(i);
                    i--;//避免錯砍
                }
            }

            if(enmeies.Count ==0)
            {
                theRoom.OpenDoor();
            }
        }
    }
}
