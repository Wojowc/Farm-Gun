using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Start");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Lobby");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Load");
        SceneManager.LoadScene("Lobby");
    }
}
