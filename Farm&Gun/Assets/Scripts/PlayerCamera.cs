using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float xFromPlayer, zFromPlayer;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xFromPlayer, transform.position.y, player.transform.position.z + zFromPlayer);
    }
}
