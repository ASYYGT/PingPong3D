using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class racketsMove : MonoBehaviour
{

    private PhotonView pv;
    //Tus atamalari
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    //Raket hizi ve siniri
    public float speed = 10.0f;
    public float boundY = 4.3f;

    private Rigidbody rb;


    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        if (pv.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(4, 0.5f,0);
                InvokeRepeating("Control", 0, 0.05f);

            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-4, 0.5f , 0);
            }
        }

    }
    [PunRPC]
    //Oyuncu cikinca topun konumunu yeniden ortalýyoruz ve oyun durakliyor.
    public void leavePlayer()
    {
        InvokeRepeating("Control", 0, 0.5f);
        GameObject.Find("Ball").GetComponent<PhotonView>().RPC("ExitPlayer", RpcTarget.All, null);
    }
    //Oyuncu controlu : 2 kisi baglandiginda oyun basliyor.
    void Control()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.Find("Ball").GetComponent<PhotonView>().RPC("GoBall", RpcTarget.AllBuffered, null);
            CancelInvoke("Control");
        }
    }

    //oyun icerisinde surekli guncel olmasini istedigimiz olaylari atadik.
    void Update()
    {

        if (pv.IsMine)
        {
            moveRacket();
        }
    }


    private void moveRacket()
    {
        //hareketler
        var vel = rb.velocity;
        if (Input.GetKey(moveUp))
        {
            vel.z = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.z = -speed;
        }
        else
        {
            vel.y = 0;
        }
        rb.velocity = vel;
    }
}
