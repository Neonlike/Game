using UnityEngine.UI;
using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject PlayerInfoText;
    [SerializeField]
    private GameObject PlayerGrid;
    [SerializeField]
    private Text playerPing;

    public Text playersScoreInfotxt;

    public Text kdTextInfo;

    public Text starsCollectedInfoTxt;

    void Update()
    {
        starsCollectedInfoTxt.text = PhotonNetwork.player.GetScore().ToString();
        kdTextInfo.text = PhotonNetwork.player.GetKills().ToString() + " \\ " + PhotonNetwork.player.GetDeathes().ToString();
        UpdateScoreBoardNew();
        playerPing.text ="ping: " + PhotonNetwork.GetPing().ToString();
    }
    
    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {

        PhotonNetwork.player.SetWeapon(PlayerPrefs.GetInt("WeaponID"));
       
        GameObject obj = Instantiate(PlayerInfoText, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(PlayerGrid.transform, false);
        obj.GetComponent<Text>().text = PhotonNetwork.playerName + " joined the game";
        obj.GetComponent<Text>().color = Color.green;
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    { 

        GameObject obj = Instantiate(PlayerInfoText, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(PlayerGrid.transform, false);
        obj.GetComponent<Text>().text = PhotonNetwork.playerName + " left the game";
        obj.GetComponent<Text>().color = Color.red;
    }

    void UpdateScoreBoardNew()
    {
     //   playerCount = PhotonNetwork.playerList.Length;

        var playerList = new StringBuilder();
        var myPlayerList = PhotonNetwork.playerList.ToList();
        var newPlayerList = myPlayerList.OrderByDescending(x => x.GetMainScore());
        foreach (PhotonPlayer pl in newPlayerList)
        {
        
            playerList.Append(pl.NickName + "\t" + pl.GetMainScore() +"\n");

        }


        string outputi = "Players: " + "\t" + "Score:" + "\n"
            + playerList.ToString();

        playersScoreInfotxt.text = outputi; 
       // playersScoreInfotxt.text = outputi;
    }

   

}
