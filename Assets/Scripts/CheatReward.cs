using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CheatCode))]
public class CheatReward : MonoBehaviour
{
    private CheatCode code;
    private CheatReward instance;
    public GameObject[] cheatGuns;
    public Transform[] cheatGunsSpawnPoint;
    void Awake()
    {
        instance = this;
        code = GetComponent<CheatCode>();
    }

    void Update()
    {
        if (code.success)
        {
            for (int i = 0; i < cheatGuns.Length; i++)
            {
                Instantiate(cheatGuns[i], cheatGunsSpawnPoint[i].position, cheatGunsSpawnPoint[i].rotation);
            }
            code.success = false;
            instance.enabled = !instance.enabled;
            //code.enabled = !code.enabled;
        }
    }
}
