using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager 
{
    public static int SteeringNumber
    {
        get { return PlayerPrefs.GetInt("SteeringNumber"); }
        set { PlayerPrefs.SetInt("SteeringNumber", value); }
    }

    public static int StickNumber
    {
        get { return PlayerPrefs.GetInt("StickNumber"); }
        set { PlayerPrefs.SetInt("StickNumber", value); }
    }
    public static int DashBoardItemNumber
    {
        get { return PlayerPrefs.GetInt("DashBoardItemNumber"); }
        set { PlayerPrefs.SetInt("DashBoardItemNumber", value); }
    }
    public static int TrinketNumber
    {
        get { return PlayerPrefs.GetInt("TrinketNumber"); }
        set { PlayerPrefs.SetInt("TrinketNumber", value); }
    }
    public static int StickerNumber
    {
        get { return PlayerPrefs.GetInt("StickerNumber"); }
        set { PlayerPrefs.SetInt("StickerNumber", value); }
    }
    public static float Stars
    {
        get { return PlayerPrefs.GetFloat("Stars"); }
        set { PlayerPrefs.SetFloat("Stars", value); }
    }


    public static int LevelCompleteVehicle
    {
        get { return PlayerPrefs.GetInt("LevelCompleteVehicle"); }
        set { PlayerPrefs.SetInt("LevelCompleteVehicle", value); }
    }

    public static int LevelNumber
    {
        get { return PlayerPrefs.GetInt("LevelNumber"); }
        set { PlayerPrefs.SetInt("LevelNumber", value); }
    }

    public static int AvailableStickers
    {
        get { return PlayerPrefs.GetInt("AvailableStickers"); }
        set { PlayerPrefs.SetInt("AvailableStickers", value); }
    }
    public static float Keys
    {
        get { return PlayerPrefs.GetFloat("Keys"); }
        set { PlayerPrefs.SetFloat("Keys", value); }
    }

    public static int Money
    {
        get { return PlayerPrefs.GetInt("Money"); }
        set { PlayerPrefs.SetInt("Money", value); }
    }

    public static int Music
    {
        get { return PlayerPrefs.GetInt("Music"); }
        set { PlayerPrefs.SetInt("Music", value); }
    }

    public static int SFX
    {
        get { return PlayerPrefs.GetInt("SFX"); }
        set { PlayerPrefs.SetInt("SFX", value); }
    }

    public static int Vib
    {
        get { return PlayerPrefs.GetInt("Vib"); }
        set { PlayerPrefs.SetInt("Vib", value); }
    }

    public static int Crane
    {
        get { return PlayerPrefs.GetInt("crane"); }
        set { PlayerPrefs.SetInt("crane", value); }
    }

    public static int Button
    {
        get { return PlayerPrefs.GetInt("Button"); }
        set { PlayerPrefs.SetInt("Button", value); }
    }
    public static int PP
    {
        get { return PlayerPrefs.GetInt("PP"); }
        set { PlayerPrefs.SetInt("PP", value); }
    }
}
