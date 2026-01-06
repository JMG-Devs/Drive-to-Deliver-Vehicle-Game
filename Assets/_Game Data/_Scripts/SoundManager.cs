using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource vehicleLoopSource;
    public AudioSource vehicleBGSource;

    public AudioClip[] GamePlayMusic;
    public AudioClip ShopMusic;
    public AudioClip ButtonSound;
    public AudioClip LevelCompleteSound;
    public AudioClip LevelFailedSound;
    public AudioClip CheckPointSound;
    public AudioClip PuffSound;
    public AudioClip ItemUnlockSound;
    public AudioClip ShopUnlockSound;
    public AudioClip SelectionSound;
    public AudioClip KeySound;
    public AudioClip AfterCardClickedSound;
    public AudioClip CardClickedSound;
    public AudioClip ChestOpenSound;
    public AudioClip ChestSlideSound;
    public AudioClip PurchaseSound;
    public AudioClip ForwardSound;
    public AudioClip ReverseBeep;
    public AudioClip cashSound;
    // Additional audio clips for runner platformer


    [Header("Npc Hit Clips")]
    public AudioClip[] NpcHitClips;


    int rand;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        UpdateVolumes();
        GamePlayMusicPlay();
    }

    public void UpdateVolumes()
    {


        if (PrefsManager.SFX == 0)
        {
            sfxSource.volume = 1f;
            vehicleLoopSource.volume = 0.5f;
            vehicleBGSource.volume = 0.2f;

        }
        else
        {
            sfxSource.volume = 0;
            vehicleLoopSource.volume = 0;
            vehicleBGSource.volume = 0;
        }

        if (PrefsManager.Music == 0)
        {
            musicSource.volume = 0.75f;
        }
        else
        {
            musicSource.volume = 0f;
        }

    }


    private void PlaySFX(AudioClip sfxClip, float decible)
    {
        sfxSource.PlayOneShot(sfxClip, decible);
    }


    public void SwitchToReverse()
    {
        vehicleLoopSource.clip = ReverseBeep;
        if (PrefsManager.SFX == 0)
        {
            vehicleLoopSource.volume = 0.1f;
        }
        else
        {
            vehicleLoopSource.volume = 1;
        }
    }

    public void SwitchToForwrd()
    {
        vehicleLoopSource.clip = ForwardSound;
        if (PrefsManager.SFX == 0)
        {
            vehicleLoopSource.volume = 0.5f;
        }
        else
        {
            vehicleLoopSource.volume = 1;
        }
    }

    public void GamePlayMusicPlay()
    {
        musicSource.Stop();
        rand = Random.Range(0, 2);
        musicSource.clip = GamePlayMusic[rand];
        musicSource.Play();

    }
    public void ShopMusicPlay()
    {
        musicSource.Stop();
        musicSource.clip = ShopMusic;
        musicSource.Play();
    }




    public void LevelFailPlay()
    {
        // musicSource.Stop();
        sfxSource.PlayOneShot(LevelFailedSound, 1f);
    }


    public void LevelCompletePlay()
    {
        // musicSource.Stop();
        sfxSource.PlayOneShot(LevelCompleteSound, 1f);

    }


    public void UnlockPlay()
    {
        sfxSource.PlayOneShot(ItemUnlockSound, 1f);

    }

    public void ChestPlay()
    {
        sfxSource.PlayOneShot(ChestOpenSound, 1f);

    }
    public void ChestSlidePlay()
    {
        sfxSource.PlayOneShot(ChestSlideSound, 1f);

    }

    public void AfterCardClickedPlay()
    {
        sfxSource.PlayOneShot(AfterCardClickedSound, 1f);

    }
    public void CardClickedPlay()
    {
        sfxSource.PlayOneShot(CardClickedSound, 1f);

    }

    public void KeyPlay()
    {
        sfxSource.PlayOneShot(KeySound, 1f);

    }
    public void CashPlay()
    {
        sfxSource.PlayOneShot(cashSound, 1f);
    }
  
    public void ShopUnlockPlay()
    {
        sfxSource.PlayOneShot(ShopUnlockSound, 1f);

    }
    public void SelectionPlay()
    {
        sfxSource.PlayOneShot(SelectionSound, 1f);

    }

    public void PuffPlay()
    {
        sfxSource.PlayOneShot(PuffSound, 1f);

    }

    public void CheckPointPlay()
    {
        sfxSource.PlayOneShot(CheckPointSound, 1f);

    }

    public void PurchasePlay()
    {

        sfxSource.PlayOneShot(PurchaseSound, 1f);
    }



    public void ButtonPlay()
    {
        sfxSource.PlayOneShot(ButtonSound, 1f);
    }

}
