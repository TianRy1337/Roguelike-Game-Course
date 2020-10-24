using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;/*,openWhenEnemiesCleared*/

    public GameObject[] doors;

   //public List<GameObject> enmeies = new List<GameObject>();
    [HideInInspector]
    public bool roomActive;

    public GameObject mapHider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if(enmeies.Count > 0 && roomActive && openWhenEnemiesCleared)
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
                foreach(GameObject door in doors)
                {
                    door.SetActive(false);

                    closeWhenEntered = false;
                }
            }
        }*/
    }
    public void OpenDoor()
    {
        foreach(GameObject door in doors)
            {
                door.SetActive(false);

                closeWhenEntered = false;
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            CameraController.instance.ChangeTarget(transform);

            if(closeWhenEntered)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            roomActive =true;

            mapHider.SetActive(false);
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            roomActive= false;
        }
    }
}
