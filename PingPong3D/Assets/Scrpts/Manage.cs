using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;



public class Manage : MonoBehaviour
{
    public Toggle twoPlayers, fourPlayers;
    public InputField roomN, userN,roomKey;
    private byte maxP;
    public static string name = "";

    public void fourPlayersT()
    {
        if (fourPlayers.isOn == true)
        {
            if (twoPlayers.isOn == true)
            {
                twoPlayers.isOn = false;
            }
            maxP = 4;

            Debug.Log(maxP);
        }
    }
    public void twoPlayersT()
    {

        if (twoPlayers.isOn == true)
        {
            if (fourPlayers.isOn == true)
            {
                fourPlayers.isOn = false;
            }
            maxP = 2;
            Debug.Log(maxP);
        }


    }
    private void Awake()
    {
        

    }
   



    public void QuickGame()
    {
        name = userN.text;

        

        if (userN.text != "")
            {
                PhotonNetwork.JoinRandomRoom();  //herhangi kurulmuþ random odaya gir.
            
                SceneManager.LoadScene("Game"); //Oyun sahnesini yükle.
            }
            else
            {
                Debug.Log("Ýsim Girilmedi.");
            }



    }

    public void CreateGame()
    {
        name = userN.text;

        if (userN.text != "" && roomN.text != "")
            {
                
            PhotonNetwork.CreateRoom(roomN.text, new Photon.Realtime.RoomOptions() { MaxPlayers = maxP }, Photon.Realtime.TypedLobby.Default); //Yeni oda olustur.
              
            SceneManager.LoadScene("Game"); //Oyun sahnesini yükle.
            }
        else
            {
                Debug.Log("Ýsim Girilmedi.");
            }
 
    }


    public void GoGame(string roomK)
    {
        name = userN.text;

        roomK = roomKey.text;
            if (roomK != "" && userN.text != "")
            {
                PhotonNetwork.JoinRoom(roomK);
            

                SceneManager.LoadScene("Game");
            }
            else
            {
                Debug.Log("Ýsim Gir.");
            }

    }

    public void ExitGame()
    {
        Application.Quit(); //Uygulamadan cik.
    }
   
    void Start()
    {
        PhotonNetwork.JoinLobby(); //Eger lobiden cikma durumu olursa yeniden baglanmasi icin bir kez daha burda kullandik.

    }


}


