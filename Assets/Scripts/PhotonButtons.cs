using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonButtons : MonoBehaviour {

    byte players=2;
    public GameObject matchSearching;
    public Text readysPlayersTxt;
    public GameObject playerTypePanel, playerWeaponList, playerBodyList;
    public GameObject settingsPanel;


    public void OnClickStartMatchSolo()
    {

        players = 1;
        print("Searching the game...");

        PlayersSearching();

        PhotonNetwork.JoinRandomRoom(null, players);

    }

    public void OnClickStartMatch1v1()
    {

        players = 2;
        print("Searching the game...");

        PlayersSearching();

        PhotonNetwork.JoinRandomRoom(null, players);
        //Debug.LogError("No rooms, so we created one");
        
    }
    public void OnClickStartMatch2v2()
    {
        players = 4;
        print("Searching the game...");

        PlayersSearching();

        PhotonNetwork.JoinRandomRoom(null, players);
    }
    public void OnClickStartMatchDeathMatch10()
    {
        players = 10;
        print("Searching the game...");

        PlayersSearching();

        PhotonNetwork.JoinRandomRoom(null, players);
    }
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = players }, null);
        
    }

    void PlayersSearching()
    {
        if (matchSearching.activeSelf == false)
            matchSearching.SetActive(true);
        
    }

    public void OnClickLeaveMatch()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            MyPhotonHandler.Instance.GetComponent<PhotonView>().RPC("LeftTheGame", PhotonTargets.All);
        }
        if (PhotonNetwork.inRoom)
        PhotonNetwork.LeaveRoom();

    }

    public void OnClickChooseTypePlayer()
    {
        if (!playerTypePanel.activeSelf)
        {
            playerTypePanel.SetActive(true);
        }
    }

    public void OnClickChooseWeapon()
    {
        if (!playerWeaponList.activeSelf)
        playerWeaponList.SetActive(true);
    }

    public void OnClickChooseBody()
    {
        Debug.Log("OnClickChooseBody");
    }


    public void OnClickBack(int b)
    {
        if (b == 0)
        {
            if (playerTypePanel.activeSelf)
            {
                playerTypePanel.SetActive(false);
            }
        }
        else 
            if (b == 1)
        {
            if (playerWeaponList.activeSelf)
            {
                playerWeaponList.SetActive(false);
            }
        }
        else
            if (b == 2)
            {
                if (playerBodyList.activeSelf)
                {
                    playerBodyList.SetActive(false);
                }
            }
    }
    public void OnClickSettings()
    {
        if (!settingsPanel.activeSelf)
        settingsPanel.SetActive(true);
        else settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (matchSearching!=null && matchSearching.activeSelf && readysPlayersTxt != null && PhotonNetwork.inRoom)
            readysPlayersTxt.text = PhotonNetwork.playerList.Length.ToString() + " \\ " + PhotonNetwork.room.MaxPlayers.ToString();
    }
}
