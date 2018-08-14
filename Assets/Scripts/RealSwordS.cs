using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealSwordS : Photon.MonoBehaviour {

    Vector3 swordScale;
    GameObject target;
    float mainScore;
    public float levelOfTarget=1.1f;
    void Update()
    {
        //swordScale = GetComponent<Transform>().localScale;
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        // if (!photonView.isMine) return;

        // PhotonView target = gameObject.GetComponent<PhotonView>();
        //  if (target != null && (!target.isMine || target.isSceneView))
        //  {
        if (photonView.isMine)
        {
            if (col.gameObject.tag == "Sword")
            {
                return;
            }
            if (col.gameObject.tag == "Player")
            {
                target = col.gameObject;
                Debug.Log("trigere on player entered!");
                
               
                    StartCoroutine(Delayka());
                    
                //PhotonNetwork.player.AddScore(5);
            }
        }
    }

     float CountMainScore()
     {
         if (photonView.isMine)
         {
             mainScore = 10 * (GetComponentInParent<ColorChanger>().LevelOfSkin + target.gameObject.GetComponent<ColorChanger>().LevelOfSkin);
         }
             return mainScore;
         
     }

     IEnumerator Delayka()
     {
         Debug.Log("DELAY Started");
         yield return new WaitForSeconds(1f);


         if (target.GetComponent<PlayerSc>().isDied)
         {
             PhotonNetwork.player.AddKills(1);
             PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1);
             PhotonNetwork.player.AddMainScore((int)CountMainScore());
         }
     }
}
