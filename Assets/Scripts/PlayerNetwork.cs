using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : Photon.MonoBehaviour {

    public GameObject myCamera;
    public GameObject myBody;
    public Text playerName;
    public Color enemyTextColor;
    private Vector3 selfPos;
    //public GameObject playCanvas;
    void Start()
    {
        if (photonView.isMine)
        {
            //    playCanvas.SetActive(true);
            
            playerName.text = PhotonNetwork.playerName;
            gameObject.name = "Me";
            //  GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
            myBody.gameObject.GetComponent<PlayerRotation>().enabled = true;
            myCamera.SetActive(true);
            GetComponent<PlayerSc>().enabled = true;
            if (GetComponent<PlayerWeapon>())
            GetComponent<PlayerWeapon>().enabled = true;
            
        }
        else
        {
            playerName.text = photonView.owner.NickName;
            gameObject.name = "Network player";
            playerName.color = Color.red;
            //   gameObject.tag = "Network player";

        }
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            smoothNetMoveMent();
        }
        if (photonView.isMine)
        {
            if (!myBody.activeSelf)
            {
                if (myCamera.activeSelf)
                    myCamera.SetActive(false);
            }
            else if (!myCamera.activeSelf)
                myCamera.SetActive(true);
        }
    }

    void smoothNetMoveMent()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 14);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else selfPos = (Vector3)stream.ReceiveNext();
    }
}
