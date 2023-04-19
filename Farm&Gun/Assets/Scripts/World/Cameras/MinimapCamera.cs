using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void Update()
    {
        //minimap camera will be following the player from the top
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
