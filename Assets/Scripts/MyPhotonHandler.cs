using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MyPhotonHandler : Photon.MonoBehaviour
{

    string nameScene = "Mode1v1";
    public static MyPhotonHandler Instance;

   // public GameObject mainPlayer;

    private byte playersInGame = 0;
    
    Transform spawnPointToTransform;



    void Awake()
    {
        SpriteLoader.LoadAllSprites("OtherSprites");
        SpriteLoader.LoadAllSprites("WeaponSprite");
        SpriteLoader.LoadAllSprites("WeaponSpriteIc");
    }
    

    void Start()
    {
        PhotonNetwork.sendRate = 60; // 30
        PhotonNetwork.sendRateOnSerialize = 60; //28

        Instance = this;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        DontDestroyOnLoad(this.transform);
    }

    //Photon methode
    void OnJoinedRoom()
    {
        PhotonNetwork.player.SetScore(0);
        PhotonNetwork.player.SetKills(0);
        PhotonNetwork.player.SetDeathes(0);
        PhotonNetwork.player.SetMainScore(0);

        playersInGame = 0;
        //if (PhotonNetwork.playerList.Length == PhotonNetwork.room.pl)
        print("Joined the room");

        PhotonNetwork.playerName = "Player№" + Random.Range(10, 99);

        print(PhotonNetwork.playerList.Length.ToString());
        print(PhotonNetwork.room.MaxPlayers.ToString());

        if (PhotonNetwork.room.MaxPlayers == 10)
        {
            print("DeathMatch...");
            nameScene = "Deathmatch";
            moveScene();
        }
        if (PhotonNetwork.playerList.Length == PhotonNetwork.room.MaxPlayers)
        {
            print("All players are connected...");
            if (PhotonNetwork.playerList.Length == 1)
            {
                print("Ready to downloading scene...");
                nameScene = "Mode1";
                moveScene();
                // GetComponent<PhotonView>().RPC("moveScene", PhotonTargets.All);
            }
            if (PhotonNetwork.playerList.Length == 2)
            {
                print("Ready to downloading scene...");
                nameScene = "Mode1v1";
                moveScene();
                //   GetComponent<PhotonView>().RPC("moveScene", PhotonTargets.All);
            }
            if (PhotonNetwork.playerList.Length == 4)
            {
                nameScene = "Mode2v2";
                moveScene();
                //   GetComponent<PhotonView>().RPC("moveScene", PhotonTargets.All);
            }
        }
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        print("Another one joined the room");
        //Doesn't get called on the local player, just remote players, so you would still need something to handle on the second player
        if (PhotonNetwork.playerList.Length == PhotonNetwork.room.MaxPlayers)
        {
            if (PhotonNetwork.playerList.Length == 2)
            {
                nameScene = "Mode1v1";
                moveScene();
            }
            if (PhotonNetwork.playerList.Length == 4)
            {
                nameScene = "Mode2v2";
                moveScene();

            }
        }
    }

    void moveScene()
    {
        print("Loading scene...");
        PhotonNetwork.LoadLevel(nameScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        
        
        if (scene.name == "Mode1v1")
        {
            GetComponent<PhotonView>().RPC("RPC_LoadedGameScene", PhotonTargets.All);
            print("Mode1v1");
        }
        else if (scene.name == "Mode2v2")
        {
            GetComponent<PhotonView>().RPC("RPC_LoadedGameScene", PhotonTargets.All);
            print("Mode2v2");
        }
        else if (scene.name == "Mode1")
        {
            GetComponent<PhotonView>().RPC("RPC_LoadedGameScene", PhotonTargets.All);
            print("Mode1");
        }
        else if (scene.name == "Deathmatch")
        {
            GetComponent<PhotonView>().RPC("RPC_LoadedGameSceneDeathmatch", PhotonTargets.All);
            StartRespawn.Instance.StartTimer();
        }
    }

    void OnLeftRoom()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            
           // GetComponent<PhotonView>().RPC("LeftTheGame", PhotonTargets.All);
            PhotonNetwork.LoadLevel(0);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            if (GameObject.Find("SearchingGamePanel"))
                GameObject.Find("SearchingGamePanel").SetActive(false);
        }
    }

    [PunRPC]
    void RPC_LoadedGameScene()
    {print("RPC_LoadedGameScene");


        playersInGame++;
      
        if (playersInGame == PhotonNetwork.playerList.Length)
        {
            //Make room invisible and closed when its alrdy started.
            PhotonNetwork.room.IsOpen = false;
            PhotonNetwork.room.IsVisible = false;
            print("All players are in the game scene");
           
            StartRespawn.Instance.StartTimer();
        }


    }

    [PunRPC]
    void RPC_LoadedGameSceneDeathmatch()
    {
        print("RPC_LoadedGameSceneDeathmatch");


        playersInGame++;
    }

    [PunRPC]
    public void LeftTheGame()
    {
        playersInGame--;
    }

    //Photon 
    void OnDisconnect()
    {
        Debug.Log("Disconnected!");
    }
}