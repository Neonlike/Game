using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorResiever : MonoBehaviour {

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.GetComponent<SpriteRenderer>().color);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = (Color)stream.ReceiveNext();
        }
    }
}
