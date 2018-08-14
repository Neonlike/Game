using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ColorChanger : Photon.PunBehaviour
{
    [SerializeField] SpriteRenderer[] m_spriteRender;
    [SerializeField] PhotonView m_pView;
    //string[] str;
    Sprite spr;
    string spriteName = "HeroParts_3";


    [SerializeField]
    SpriteRenderer[] weaponSprite; // 2 weapons in hands
    [SerializeField]
    string spriteNameID = "Dagger";

    float levelOfSkin = 1.1f;
    public float LevelOfSkin
    {
        get { return levelOfSkin; }
        set { levelOfSkin = value; }
    }

    void Start()
    {
       // SpriteLoader.LoadAllSprites("OtherSprites");
    
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        
        PhotonPlayer m_photonPlayer = (PhotonPlayer)playerAndUpdatedProps[0];
        Hashtable m_changedPropertie = (Hashtable)playerAndUpdatedProps[1];

        if (m_changedPropertie.ContainsKey(PunPlayerScores.PlayerScoreProp))
        {
            
			int m_score = (int)m_changedPropertie[PunPlayerScores.PlayerScoreProp];

            if (m_photonPlayer.ID == m_pView.OwnerActorNr)
            {
                
				ChangeColor(m_score);
            }
        }

            if (m_changedPropertie.ContainsKey(PunPlayerScores.PlayerWeaponID))
            {

                int w_Id = (int)m_changedPropertie[PunPlayerScores.PlayerWeaponID];

                if (m_photonPlayer.ID == m_pView.OwnerActorNr)
                {

                    GetComponent<PhotonView>().RPC("GetWeaponek", PhotonTargets.AllBuffered, w_Id);
                    //GetWeaponek(w_Id);
                }
            }

        
    }

    [PunRPC]
    public void GetWeaponek(int weaponID)
    {
        for (int i = 0; i < weaponSprite.Length; i++)
        {
            // if (photonView.isMine)
            weaponSprite[i].sprite = SpriteLoader.GetSprite(spriteNameID + weaponID.ToString());
            Debug.Log(spriteNameID + PlayerPrefs.GetInt("WeaponID").ToString());
        }
    }

    public void ChangeColor(int score)
    {
        
        //some default Color
       // Color m_color = Color.white;

        //your score color logic stuff herer
        if (score < 10)
        {
            LevelOfSkin = 1.1f;
               
           spriteName = "Kot-gladiator_0";

           
        }

        if (score >= 10) //&& score <= 19)
        {

            LevelOfSkin = 1.2f;  
            spriteName = "Kot-gladiator2_0";

        }
        if (score >= 20 && score <= 29)
        {
            LevelOfSkin = 1.3f;  
           // spriteName = "HeroParts7_3";
        }
        if (score >= 30 && score <= 39)
        {
            LevelOfSkin = 1.4f;  
          //  spriteName = "HeroParts2_3";

        }
        if (score >= 40 && score <= 49)
        {
            LevelOfSkin = 1.5f;  
           // spriteName = "HeroParts4_3";

        }
        if (score >= 50)
        {
            //spriteName = "HeroParts6_3";
            LevelOfSkin = 1.6f;  

        }

       
        spr = SpriteLoader.GetSprite(spriteName);
        m_spriteRender[0].sprite = spr;
       
    }
}