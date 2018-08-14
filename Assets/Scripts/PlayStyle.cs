using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStyle : MonoBehaviour {

    public GameObject weaponImg;
    public int weaponID = 1;

    void Start()
    {
        //if (PlayerPrefs.GetInt("PlayStyle") == null)
        //{
        //    PlayerPrefs.SetInt("PlayStyle", 0);
        //} 
    }

    public void SetPlayStyle(int type)
    {
        PhotonNetwork.player.SetWeapon(weaponID);

        PlayerPrefs.SetInt("PlayStyle", type);
        print("weapinID: " + weaponID);
        PlayerPrefs.SetInt("WeaponID", weaponID);
        Debug.Log("weapinID PlayerPrefs: " + PlayerPrefs.GetInt("WeaponID"));
        Debug.Log("weapinID PhotonNetwork.player.GetWeapon(): " + PhotonNetwork.player.GetWeapon());
        PlayerPrefs.Save();
        weaponImg.GetComponent<Image>().sprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;

        Debug.Log(PlayerPrefs.GetInt("PlayStyle"));
    }
}
