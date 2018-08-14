using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PunPlayerScores : MonoBehaviour
{
    public const string PlayerScoreProp = "score";
    public const string PlayerKillProp = "kills";
    public const string PlayerDeathProp = "deathes";
    public const string PlayerWeaponID = "weapon";
    public const string PlayerMainScoreProp = "mainScore";
}

public static class ScoreExtensions
{
    public static void SetScore(this PhotonPlayer player, int newScore)
    {
        Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
        score[PunPlayerScores.PlayerScoreProp] = newScore;

        player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
    }

    public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
    {
        int current = player.GetScore();
        current = current + scoreToAddToCurrent;

        Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
        score[PunPlayerScores.PlayerScoreProp] = current;

        player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
    }

    public static int GetScore(this PhotonPlayer player)
    {
        object score;
        if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerScoreProp, out score))
        {
            return (int) score;
        }

        return 0;
    }


/////////////////////////////////////////////////KILLS SCOR/////////////////////////////////////////////////////////////////

    public static void SetKills(this PhotonPlayer player, int newKills)
    {
        Hashtable kills = new Hashtable();  // using PUN's implementation of Hashtable
        kills[PunPlayerScores.PlayerKillProp] = newKills;

        player.SetCustomProperties(kills);  // this locally sets the score and will sync it in-game asap.
    }

    public static void AddKills(this PhotonPlayer player, int killsToAddToCurrent)
    {
        int current = player.GetKills();
        current = current + killsToAddToCurrent;

        Hashtable kills = new Hashtable();  // using PUN's implementation of Hashtable
        kills[PunPlayerScores.PlayerKillProp] = current;

        player.SetCustomProperties(kills);  // this locally sets the score and will sync it in-game asap.
    }

    public static int GetKills(this PhotonPlayer player)
    {
        object kills;
        if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerKillProp, out kills))
        {
            return (int)kills;
        }

        return 0;
    }

    /////////////////////////////////////////////////KILLS SCOR/////////////////////////////////////////////////////////////////

    public static void SetDeathes(this PhotonPlayer player, int newDeathes)
    {
        Hashtable deathes = new Hashtable();  // using PUN's implementation of Hashtable
        deathes[PunPlayerScores.PlayerDeathProp] = newDeathes;

        player.SetCustomProperties(deathes);  // this locally sets the score and will sync it in-game asap.
    }

    public static void AddDeathes(this PhotonPlayer player, int deathesToAddToCurrent)
    {
        int current = player.GetDeathes();
        current = current + deathesToAddToCurrent;

        Hashtable deathes = new Hashtable();  // using PUN's implementation of Hashtable
        deathes[PunPlayerScores.PlayerDeathProp] = current;

        player.SetCustomProperties(deathes);  // this locally sets the score and will sync it in-game asap.
    }

    public static int GetDeathes(this PhotonPlayer player)
    {
        object deathes;
        if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerDeathProp, out deathes))
        {
            return (int)deathes;
        }

        return 0;
    }

    /////////////////////////////////////////////////PlayerWeaponID/////////////////////////////////////////////////////////////////

    public static void SetWeapon(this PhotonPlayer player, int newWeapon)
    {
        Hashtable weapon = new Hashtable();  // using PUN's implementation of Hashtable
        weapon[PunPlayerScores.PlayerWeaponID] = newWeapon;

        player.SetCustomProperties(weapon);  // this locally sets the score and will sync it in-game asap.
    }

    

    public static int GetWeapon(this PhotonPlayer player)
    {
        object weapon;
        if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerWeaponID, out weapon))
        {
            return (int)weapon;
        }

        return 0;
    }
    /////////////////////////////////////////////////PlayerMainScoreID/////////////////////////////////////////////////////////////////

    public static void SetMainScore(this PhotonPlayer player, int newMainScore)
    {
        Hashtable mainScore = new Hashtable();  // using PUN's implementation of Hashtable
        mainScore[PunPlayerScores.PlayerMainScoreProp] = newMainScore;

        player.SetCustomProperties(mainScore);  // this locally sets the score and will sync it in-game asap.
    }

    public static void AddMainScore(this PhotonPlayer player, int mainScoreToAdd)
    {
        int current = player.GetMainScore();
        current = current + mainScoreToAdd;

        Hashtable mainScore = new Hashtable();  // using PUN's implementation of Hashtable
        mainScore[PunPlayerScores.PlayerMainScoreProp] = current;

        player.SetCustomProperties(mainScore);  // this locally sets the score and will sync it in-game asap.
    }

    public static int GetMainScore(this PhotonPlayer player)
    {
        object mainScore;
        if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerMainScoreProp, out mainScore))
        {
            return (int)mainScore;
        }

        return 0;
    }
}