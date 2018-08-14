using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : Photon.MonoBehaviour {

    public GameObject localPlayer_canvas;
    public Image localPlayer_energyBar;
    public Image otherPlayer_energyBar;

    void Awake()
    {
        if (photonView.isMine)
        {
            localPlayer_canvas.SetActive(true);


        }

    }




    public void reduceEnergyAmount(float loseEnergy)
    {
        if (photonView.isMine)
        {
            localPlayer_energyBar.fillAmount -= loseEnergy;
        }
        else { otherPlayer_energyBar.fillAmount -= loseEnergy; }
    }

    [PunRPC]
    public void reduceEnergy(float reducedEnergy)
    {
        reduceEnergyAmount(reducedEnergy);
    }

}
