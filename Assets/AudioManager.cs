using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioSource background_music;
    //There are 6 possible icons that will be swapped, 2 per holder
    public Sprite[] muteIcons;
    // There will be three icons that'll be toggled
    public Image[] iconHolders;

    public bool sfxBool = true;
    public bool bgmBool = false;
    public bool vibBool = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void muteBGM()
    {
        bgmBool = !bgmBool;

        if (bgmBool == true)
        {
            background_music.mute = true;
            iconHolders[1].sprite = muteIcons[1];
        }


        else if (bgmBool == false)
        {
            background_music.mute = false;
            iconHolders[1].sprite = muteIcons[0];
        }

    }

    public void toggleSFX()
    {

        sfxBool = !sfxBool;
        if (sfxBool == true)
        {
            //background_music.mute = true;
            iconHolders[0].sprite = muteIcons[2];
        }


        else if (sfxBool == false)
        {
            // background_music.mute = false;
            iconHolders[0].sprite = muteIcons[3];
        }

    }

    public void togglVibration()
    {
        vibBool = !vibBool;
        if (vibBool == true)
        {
            //background_music.mute = true;
            iconHolders[2].sprite = muteIcons[4];
        }


        else if (vibBool == false)
        {
            // background_music.mute = false;
            iconHolders[2].sprite = muteIcons[5];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
