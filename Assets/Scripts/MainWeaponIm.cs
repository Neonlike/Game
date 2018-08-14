using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWeaponIm : MonoBehaviour
{
    string weaponName = "Sword";

    void Start()
    {
        if (PlayerPrefs.GetInt("PlayStyle") == 0)
        {
            weaponName = "Sword";
        }
        else
        if (PlayerPrefs.GetInt("PlayStyle") == 1)
        {
            weaponName = "Swords";
        } 
        else
        if (PlayerPrefs.GetInt("PlayStyle") == 2)
        {
            weaponName = "Dagger";
        }
        else
        if (PlayerPrefs.GetInt("PlayStyle") == 3)
        {
            weaponName = "BigSword";
        }

        gameObject.GetComponent<Image>().sprite = SpriteLoader.GetSprite(weaponName + PlayerPrefs.GetInt("WeaponID").ToString() + "Ic"); ;
    }

}
