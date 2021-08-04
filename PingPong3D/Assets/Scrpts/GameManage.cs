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
        Debug.Log("Sunucuya Ba�lan�ld�.");

        //lobiye ba�lanma 
        PhotonNetwork.JoinLobby();
        //Debug.Log("giri� sa�land�");

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
        Debug.Log("Lobiden ayr�ld�.");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("Odadan ayr�ld�.");
        SceneManager.LoadScene("Menu");
    }
    //HATA FONK.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Herhangi bir odaya giri� yap�lamad�.");

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
        Debug.Log("Odaya Olu�turlamad�.");
    }
    //Oyuncu odadan cikar ise
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Oyuncu ��kt�.");
        GameObject.FindWithTag("Player").GetComponent<PhotonView>().RPC("leavePlayer", RpcTarget.All, null);
    }

}
