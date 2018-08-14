using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRespawn : Photon.MonoBehaviour {

    public static StartRespawn Instance;

    public GameObject respawnCanvas;
    public GameObject[] playCanvasElements;
    public GameObject waitingPlayersPanel;
    public GameObject[] mainPlayer;

    public Text timeText;
    [SerializeField]
    private float timerAmount = 5.5f;

    private bool enableTimer = false;

   // private int numberOfPlayers;

    void Awake()
    {
      //  waitingPlayersPanel.SetActive(true);
        Instance = this;
       // numberOfPlayers = PhotonNetwork.countOfPlayers;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (enableTimer)
        {
            timerAmount -= Time.deltaTime;
            timeText.text = timerAmount.ToString("F0");
            if (timerAmount <= 0)
            {
                for (int i = 0; i < playCanvasElements.Length; i++)
                {
                    if (playCanvasElements[i].activeSelf == false)
                        playCanvasElements[i].SetActive(true);
                }

                spawnPlayer();
               // mainPlayer[PlayerPrefs.GetInt("PlayStyle")].GetComponent<PhotonView>().photonView.RPC("respawnPlayer", PhotonTargets.All);
                respawnCanvas.SetActive(false);
                enableTimer = false;
            }
            
        }

    }

    public void StartTimer()
    {
        if(waitingPlayersPanel.activeSelf)
        waitingPlayersPanel.SetActive(false);
        timerAmount = 5.5f;
        respawnCanvas.SetActive(true);
        enableTimer = true;
    }

    
    private void spawnPlayer()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        
        PhotonNetwork.Instantiate(mainPlayer[PlayerPrefs.GetInt("PlayStyle")].name, 
            spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, 
            mainPlayer[PlayerPrefs.GetInt("PlayStyle")].transform.rotation, 0);
        

    }

   
}
