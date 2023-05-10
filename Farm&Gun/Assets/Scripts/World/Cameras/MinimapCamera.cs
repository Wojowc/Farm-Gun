using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            //minimap camera will be following the player from the top
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
        
    }
}
