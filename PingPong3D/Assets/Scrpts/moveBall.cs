using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moveBall : MonoBehaviour
{

    public int PlayerScore1 = 0, PlayerScore2 = 0;

    public Text Score1, Score2, Ping, RoomKeyText;

    private Rigidbody rb;
    public static string Key;

    private void ScoreTable()
    {
        Score1.text = PhotonNetwork.PlayerList[1].NickName + ": " + PlayerScore2.ToString();
        Score2.text = PhotonNetwork.PlayerList[0].NickName + ": " + PlayerScore1.ToString();
    }
    private void Update()
    {
        Ping.text = PhotonNetwork.GetPing().ToString() + "Ms"; //Ping Text
    }

    private PhotonView pv;
    [PunRPC]
    public void ExitPlayer() //Oyuncu ciktiginda topun konumu sifirlanir.
    {
        rb.velocity = new Vector3(0,0.5f,0);
        transform.position = new Vector3(0, 0.5f, 0);
    }


    [PunRPC]


    public void GoBall()
    {
        ScoreTable();
        Debug.Log("Top hareketlendi");
        //Topun Sag ya da Sol sahaya gitmesi ve giderken bu degerler arasinda gitmesi.
        float rand = Random.Range(0, 2); //sag ya da sola gitme olayi.

        while (true)
        {
            float randx = Random.Range(-10, 10); //x ekseninde -25 ila 25 arasýnda itme gucu uyguluyoruz.
            float randz = Random.Range(-10, 10); // y ekseninde ....
            if (randx > 5 || randx < -5) // topu her defasýnda farklý hiz olsun istedik, x degerinin az olmasi top yavas hareket ediyor. 
            {
                if (rand < 1)
                {
                    rb.velocity = new Vector3(randx, 0, randz);
                    //rb.AddForce(new Vector3(randx, 0, randz) ); //itme gucu uygulanýyor.
                    break;
                }
                else
                {
                    rb.velocity = new Vector3(randx, 0, randz);
                    //rb.AddForce(new Vector3(randx, 0, randz));
                    break;
                }
            }



        }


}

    [PunRPC]

    //Gol olunca uygulanan islemler.
    public void goal(int Player1 = 0, int Player2 = 0)
    {

        PlayerScore1 += Player1;
        PlayerScore2 += Player2;


        if (PlayerScore1 == 5 || PlayerScore2 == 5)
        {
            RestartGame();
        }
        else
        {
            ResetBall();
            GoBall();
        }


    }
    //Odadan cikis fonk.
    public void GoLobby() 
    {
        PhotonNetwork.LeaveRoom();
    }
    //Herhangi bir oyuncu kazaninca oyun yeniden basliyor.
    public void RestartGame()
    {
        if (PlayerScore1 == 5)
        {
            Score2.text = PhotonNetwork.PlayerList[0].NickName + ": " + PlayerScore1.ToString() + "\nWINNER";

        }
        else if (PlayerScore2 == 5)
        {
            Score1.text = PhotonNetwork.PlayerList[1].NickName + ": " + PlayerScore2.ToString() + "\nWINNER";
        }
        PlayerScore1 = 0;
        PlayerScore2 = 0;
        ResetBall();
        Invoke("GoBall", 5.0f);

    }

    //Nesneleri tanimladik.
    void Start()
    {
        RoomKeyText = GameObject.Find("Canvas/RoomKeyText").GetComponent<Text>();
        RoomKeyText.text = "" + Key;
        Debug.Log(RoomKeyText.name);

        rb = GetComponent<Rigidbody>();


        pv = GetComponent<PhotonView>();
    }

    public void ResetBall()
    {
        rb.velocity = new Vector3(0, 0.5f, 0);
        transform.position = new Vector3(0, 0.5f, 0);
    }
    //Sag ve Sol duvar'a top carpinca
    void OnCollisionEnter(Collision coll)
    {
        if (pv.IsMine)
        {
            if (coll.gameObject.name == "RightWall")
            {
                pv.RPC("goal", RpcTarget.AllBuffered, 0, 1);

            }
            else if (coll.gameObject.name == "LeftWall")
            {
                pv.RPC("goal", RpcTarget.AllBuffered, 1, 0);
            }

        }





    }

}
