using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(50, 70), 5, Random.Range(50, 70));
        GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        foreach(Camera cam in myPlayer.GetComponentsInChildren<Camera>())
        {
            cam.enabled = true;
        }
    }
}
