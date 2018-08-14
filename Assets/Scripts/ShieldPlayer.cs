using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPlayer : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<PhotonView>().RPC("ChangePosition", PhotonTargets.AllBuffered);
        }
    }


    [PunRPC]
    void ChangePosition()
    {
        transform.position = new Vector3(Random.Range(-16.5f, 16f), Random.Range(-10.5f, 8f), 0f);
    }
}
