using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsPanel : MonoBehaviour
{
    public GameObject MusicOn, MusicOff, SfxOn, SfxOff,VibOn,VibOff;
    public SoundManager soundManager;

    private void OnEnable()
    {
        CheckSfxButtons();
        CheckMusicButtons();
        CheckVibButtons();
       
     }


    public void ToggleSFX(int index)
    {
        PrefsManager.SFX = index;
        CheckSfxButtons();
        soundManager.ButtonPlay();
        soundManager.UpdateVolumes();
    }

    public void ToggleMusic(int index)
    {
        PrefsManager.Music = index;
        CheckMusicButtons();
        soundManager.ButtonPlay();
        soundManager.UpdateVolumes();
    }
    public void ToggleVib(int index)
    {
        PrefsManager.Vib = index;
        CheckVibButtons();
        soundManager.ButtonPlay();
    }


    public void CheckSfxButtons()
    {
        if (PrefsManager.SFX == 0)
        {
            SfxOn.SetActive(true);
            SfxOff.SetActive(false);
        }
        else
        {
            SfxOn.SetActive(false);
            SfxOff.SetActive(true);
        }
    }

    public void CheckMusicButtons()
    {
        if (PrefsManager.Music == 0)
        {
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
        }
        else
        {
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);
        }
    }

    public void CheckVibButtons()
    {
        if (PrefsManager.Vib == 0)
        {
            VibOn.SetActive(true);
            VibOff.SetActive(false);
        }
        else
        {
            VibOn.SetActive(false);
            VibOff.SetActive(true);
        }
    }

}
