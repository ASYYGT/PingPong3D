using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviourPunCallbacks
{

    static GameManage admin = null;

    void Start()
    {
        if (admin == null)
        {
            admin = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Sunucuya Baðlanýldý.");

        //lobiye baðlanma 
        PhotonNetwork.JoinLobby();
        //Debug.Log("giriþ saðlandý");

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("oyuna girildi.");

        GameObject raket = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, null);
        raket.GetComponent<PhotonView>().Owner.NickName = Manage.name;
    }
    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        Debug.Log("Lobiden ayrýldý.");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("Odadan ayrýldý.");
        SceneManager.LoadScene("Menu");
    }
    //HATA FONK.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Herhangi bir odaya giriþ yapýlamadý.");

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Odaya girilemedi.");
        SceneManager.LoadScene("Menu");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Odaya Oluþturlamadý.");
    }
    //Oyuncu odadan cikar ise
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Oyuncu Çýktý.");
        GameObject.FindWithTag("Player").GetComponent<PhotonView>().RPC("leavePlayer", RpcTarget.All, null);
    }

}
