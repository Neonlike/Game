using UnityEngine.UI;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{

    public static RespawnScript Instance;

    [HideInInspector]
    public GameObject localPlayer;

    public GameObject respawnCanvas;

    public Text timeText;
    [SerializeField]
    private float timerAmount = 10;

    private bool enableTimer = false;

    void Awake()
    {

        Instance = this;
    }
    void Start()
    {

    }
    void Update()
    {
        if (enableTimer)
        {
            timerAmount -= Time.deltaTime;
            timeText.text = "Respawn in: " + timerAmount.ToString("F0");
            if (timerAmount <= 0)
            {
                localPlayer.GetComponent<PhotonView>().RPC("respawnPlayer", PhotonTargets.AllBuffered);
                respawnCanvas.SetActive(false);
                enableTimer = false;
            }
        }

    }

    public void StartRespawnTimer()
    {
        timerAmount = 8f;
        respawnCanvas.SetActive(true);
        enableTimer = true;
    }
}