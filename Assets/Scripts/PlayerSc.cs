using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSc : Photon.MonoBehaviour {

    public GameObject clicked1, clicked2;

    public bool isAttacking = false;
    public static bool isDashing = false;

    public Joystick joystick;
    public float moveSpeed;
    Vector3 position;

    public bool isDied;

    public static float timeToFinishAttack = .35f;

    public GameObject WorldSpace_Canvas;
    public GameObject Energy_Canvas;

    public PlayerEnergy energy;
    public GameObject body;
    public GameObject sword;
    private Animator playerAnim;

    //public SpriteRenderer[] localSpriteColor;

    public GameObject defEff, killEff, dashEff, shieldEnableEff;
    Rigidbody2D rb;

    [SerializeField]
    GameObject coin;

    [SerializeField]
    GameObject shield;

    GameObject target;

    float mainScore;

    public float MainScore
    {
        get { return mainScore; }
        set { mainScore = value; } 
    }

    void Awake()
    {

        clicked1 = GameObject.Find("Attack");
        clicked2 = GameObject.Find("Dash");

        playerAnim = GetComponent<Animator>();
        if (photonView.isMine && SceneManager.GetActiveScene().name == "Deathmatch")
            RespawnScript.Instance.localPlayer = this.gameObject;
    }

    

    [PunRPC]
    void deadRespawn()
    {
        isDied = true;
        

        if (photonView.isMine)
        {
            PhotonNetwork.player.AddDeathes(1);
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = (GameObject)PhotonNetwork.Instantiate(coin.name,
                    gameObject.transform.position + new Vector3(Random.Range(-2, 2f), Random.Range(-2, 2f), 0),
                    transform.rotation, 0);

                //GameObject obj = (GameObject)PhotonNetwork.Instantiate(coin.name,
                //    gameObject.transform.position, transform.rotation, 0);

            }
        }
        
        //PhotonNetwork.Destroy(gameObject);

            this.GetComponent<Rigidbody2D>().simulated = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            WorldSpace_Canvas.SetActive(false);
            Energy_Canvas.SetActive(false);

            body.SetActive(false);
        
        if (photonView.isMine)
        RespawnScript.Instance.StartRespawnTimer();

    }



    [PunRPC]
    void respawnPlayer()
    {
        isDied = false;
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        body.SetActive(true);
        if (!photonView.isMine)
        WorldSpace_Canvas.SetActive(true);
        if (photonView.isMine)
            Energy_Canvas.SetActive(true);


    }

    void Start()
    {
        if (photonView.isMine)
        {
            isDied = false;
            // PhotonNetwork.isMessageQueueRunning = true;
            Energy_Canvas.SetActive(true);
        }
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        InvokeRepeating("RegenEnergy", 0f, 0.5f);

    }
    void RegenEnergy()
    {
        if (photonView.isMine)
        {
            if (energy.localPlayer_energyBar.fillAmount < 1f)
                energy.localPlayer_energyBar.fillAmount += 0.05f;
            if (energy.localPlayer_energyBar.fillAmount >= 1f)
            {
                energy.localPlayer_energyBar.fillAmount = 1f;
            }
        }
        if (!photonView.isMine)
        {
            if (energy.otherPlayer_energyBar.fillAmount < 1f)
                energy.otherPlayer_energyBar.fillAmount += 0.05f;
            if (energy.otherPlayer_energyBar.fillAmount >= 1f)
            {
                energy.otherPlayer_energyBar.fillAmount = 1f;
            }
        }
    }

 

   
    void Update()
    {
        if (photonView.isMine)
        {
            
             rb = GetComponent<Rigidbody2D>();
             if (rb.velocity.x != 0 || rb.velocity.y != 0)
             {
                 GetComponent<Animator>().SetBool("Moving", true);
             }
             else 
             {
                 GetComponent<Animator>().SetBool("Moving", false);
             }

            if (clicked1.GetComponent<ClickedSc>().clickedIs && energy.localPlayer_energyBar.fillAmount >= 0.5f)
            {
                Attack();

            }
            if (clicked2.GetComponent<ClickedSc>().clickedIs && energy.localPlayer_energyBar.fillAmount >= 0.2f)
            {
                Dash();
            }

            position = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);



            if (!isAttacking && !isDashing)

                rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed, 0f);

        }
    }

    public void Attack()
    {
        if (isAttacking == false && !isDashing)
            StartCoroutine(AttackDelay());
    }

    public void Dash()
    {
        if (isAttacking == false && !isDashing)
        StartCoroutine(DashDelay());

    }


    IEnumerator DashDelay()
    {
        isDashing = true;
       clicked2.GetComponent<ClickedSc>().clickedIs = false;
       if (rb.velocity.x != 0 || rb.velocity.y != 0)
           GetComponent<Rigidbody2D>().AddForce(position * 13f, ForceMode2D.Impulse);
    this.GetComponent<PhotonView>().RPC("reduceEnergy", PhotonTargets.All, 0.2f);
    GameObject dashEf = (GameObject)PhotonNetwork.Instantiate(dashEff.name, gameObject.transform.position, gameObject.transform.rotation, 0);
    
        yield return new WaitForSeconds(timeToFinishAttack/2f);
        PhotonNetwork.Destroy(dashEf);
        isDashing = false;

    }

    IEnumerator AttackDelay()
    {
        sword.transform.localScale = new Vector3(1f, 1f, 1f);
       // gameObject.GetComponent<PhotonView>().RPC("SwordScale", PhotonTargets.All, 1f);
        
        GetComponent<PhotonView>().RPC("AttackTriggerAnim", PhotonTargets.All);
        clicked1.GetComponent<ClickedSc>().clickedIs = false;
        

        this.GetComponent<PhotonView>().RPC("reduceEnergy", PhotonTargets.All, 0.5f);

        GetComponent<PhotonView>().RPC("IsAttacking", PhotonTargets.All, true);
       // isAttacking = true;
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            GetComponent<Rigidbody2D>().AddForce(position * 15f, ForceMode2D.Impulse);
        }
            GameObject dashEf = (GameObject)PhotonNetwork.Instantiate(dashEff.name, gameObject.transform.position, gameObject.transform.rotation, 0);

            
            
        
        yield return new WaitForSeconds(timeToFinishAttack);
        PhotonNetwork.Destroy(dashEf);
        sword.transform.localScale = new Vector3(0f, 0f, 0f);
       // gameObject.GetComponent<PhotonView>().RPC("SwordScale", PhotonTargets.All, 0f);

        GetComponent<PhotonView>().RPC("IsAttacking", PhotonTargets.All, false);
    }

    [PunRPC]
    void IsAttacking(bool state)
    {
        isAttacking = state;
        foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>())
        tr.enabled = state;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if (!photonView.isMine) return;

         //PhotonView target = col.gameObject.GetComponent<PhotonView>();
        //  if (target != null && (!target.isMine || target.isSceneView))
        //  {
        if (photonView.isMine)
        
        if (col.gameObject.tag == "Sword")
        {

            if (!isAttacking)
            {

                if (shield.activeSelf)
                {
                    Debug.Log("Shield is active");
                    if (photonView.isMine)
                    {
                        Debug.Log("Sending info thats ShieldActive");
                        StartCoroutine(ShieldOff());
                        gameObject.GetComponent<PhotonView>().RPC("ShieldActive", PhotonTargets.AllBuffered, false);
                    }
                    Debug.Log("RETURN");
                    return;
                }
                else
                if (!shield.activeSelf)
                {
                    if (photonView.isMine)
                    {
                        //sword.transform.localScale = new Vector3(0f, 0f, 0f);
                        //gameObject.GetComponent<PhotonView>().RPC("SwordScale", PhotonTargets.All, 0f);

                       // col.gameObject.GetComponentInParent<RealSwordS>().levelOfTarget = GetComponent<ColorChanger>().LevelOfSkin;

                        if (PhotonNetwork.player.GetScore() >= 3 && PhotonNetwork.player.GetScore() <= 10)
                        {
                            PhotonNetwork.player.AddScore(-3);

                        }
                        else if (PhotonNetwork.player.GetScore() >= 11 && PhotonNetwork.player.GetScore() <= 20)
                        {
                            PhotonNetwork.player.AddScore(-5);
                        }
                        else if (PhotonNetwork.player.GetScore() >= 21 && PhotonNetwork.player.GetScore() <= 29)
                        {
                            PhotonNetwork.player.AddScore(-7);
                        }
                        else if (PhotonNetwork.player.GetScore() >= 30)
                        {
                            PhotonNetwork.player.AddScore(-10);
                        }
                        else { PhotonNetwork.player.SetScore(0); }

                    }
                }
                GameObject killEf = (GameObject)PhotonNetwork.Instantiate(killEff.name, gameObject.transform.position, gameObject.transform.rotation, 0);

               
                gameObject.GetComponent<PhotonView>().RPC("deadRespawn", PhotonTargets.AllBuffered);
              
                Destroy(killEf,2f);

            }
            else
            {
                //if (photonView.isMine)
                //    gameObject.GetComponent<PhotonView>().RPC("SwordScale", PhotonTargets.All, 0f);
                GameObject defEf = (GameObject)PhotonNetwork.Instantiate(defEff.name, gameObject.transform.position, gameObject.transform.rotation, 0);
              
                Destroy(defEf,2f);
            }
         }
            if (col.gameObject.tag == "Coin")
            {
                if (photonView.isMine)
                col.gameObject.GetComponent<PhotonView>().ownerId = gameObject.GetComponent<PhotonView>().ownerId;
                if (photonView.isMine)
                PhotonNetwork.player.AddScore(1);
                if (col.gameObject.GetComponent<PhotonView>().instantiationId != null)
                    PhotonNetwork.Destroy(col.gameObject);
              
                
            }

            if (col.gameObject.tag == "CoinInGame")
            {
                col.gameObject.transform.position = new Vector3(Random.Range(-16.5f, 16f), Random.Range(-10.5f, 8f), 0f);
                if (photonView.isMine)
                    PhotonNetwork.player.AddScore(1);
                


            }
            if (col.gameObject.tag == "Shield")
            {
                
                        gameObject.GetComponent<PhotonView>().RPC("ShieldActive", PhotonTargets.AllBuffered, true);

               

            }
        
    }

    [PunRPC]
    void ShieldActive(bool state)
    {
       // if (photonView.isMine)
            shield.SetActive(state);
    }
    

    [PunRPC]
    void AttackTriggerAnim()
    {
        

        playerAnim.SetTrigger("Attack");
    }

    

    IEnumerator ShieldOff()
    {
        GameObject shieldOffEf = (GameObject)PhotonNetwork.Instantiate(shieldEnableEff.name, gameObject.transform.position, gameObject.transform.rotation, 0);
        yield return new WaitForSeconds(1.5f);
        PhotonNetwork.Destroy(shieldOffEf);
    }
}
