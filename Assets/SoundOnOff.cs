using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOff : MonoBehaviour {

    public Sprite offImage;
    public Sprite onImage;

    void Start()
    {
        if (PlayerPrefs.GetInt("SoundOnOff", 1) == 0)
        {
            GetComponent<Image>().sprite = offImage;
        }
        else {
            GetComponent<Image>().sprite = onImage;
        }
    }

    public void SoundOnOffAction()
    {
        if (PlayerPrefs.GetInt("SoundOnOff", 1) == 0)
        {
            PlayerPrefs.SetInt("SoundOnOff", 1);
            GameObject.Find("DontDestroy").GetComponent<AudioSource>().Play();
            GetComponent<Image>().sprite = onImage;
        }
        else
        {
            PlayerPrefs.SetInt("SoundOnOff", 0);
            GameObject.Find("DontDestroy").GetComponent<AudioSource>().Pause();
            GetComponent<Image>().sprite = offImage;
        }
        PlayerPrefs.Save();
    }

 
}
