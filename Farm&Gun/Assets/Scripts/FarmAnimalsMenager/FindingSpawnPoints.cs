using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingSpawnPoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //search for spawn points and ad correct nametags

        GameObject.FindGameObjectsWithTag("FarmAnimalsSpawnPoint");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
