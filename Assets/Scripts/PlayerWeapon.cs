using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Photon.MonoBehaviour {
   
    [SerializeField]
    SpriteRenderer[] weaponSprite; // 2 weapons in hands
    //[SerializeField]
    //string spriteNameID = "Dagger"; // Have a list of sprites with names Dagger1, Dagger2, ... in Resources.
    [SerializeField]
    PhotonView m_pView;
    int weaponID;

    void Start()
    {
        PhotonNetwork.player.SetWeapon(PlayerPrefs.GetInt("WeaponID"));
       // gameObject.GetComponent<PhotonView>().RPC("SetTheWeapon", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    void SetTheWeapon()
    {
        weaponID = PlayerPrefs.GetInt("WeaponID");
        PhotonNetwork.player.SetWeapon(weaponID); // PhotonNetwork.player.SetWeapon(PlayerPrefs.GetInt("WeaponID"));
        
    }
}
