using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    Vector3 newPos;

    void Start()
    {
        StartCoroutine(EnableCollision());
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
             //   PhotonNetwork.Destroy(gameObject);
            // GetComponent<PhotonView>().RPC("DestroyRPC", PhotonTargets.All,col.gameObject);

        }
    }
}
