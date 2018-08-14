using UnityEngine.UI;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

    public Text connectTxt;
    public GameObject inLobbyPanel;
    public GameObject pHandler;
	void Start () 
    {
        if (!PhotonNetwork.connected)
        {
            
            connectTxt.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
        if (!GameObject.Find("MyPhotonHandler"))
        {
            pHandler.SetActive(true);
        }
	}

    void OnConnectedToMaster()
    {
        connectTxt.text = "CONNECTED!";

        

        PhotonNetwork.automaticallySyncScene = true;

        
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Destroy(connectTxt.gameObject, 1f);
    }

    void OnJoinedLobby()
    {
        Debug.Log("In Lobby");
        if (inLobbyPanel.activeSelf == false)
            inLobbyPanel.SetActive(true);
    }
}
