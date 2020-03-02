using UnityEngine;
using System.Collections;

public class SetDontDestroyOnLoad : MonoBehaviour {



	private static SetDontDestroyOnLoad playerInstance;

    public AudioClip[] audios;

	void Awake(){
		DontDestroyOnLoad (this);

		if (playerInstance == null) {
			playerInstance = this;
		} else {
			DestroyObject(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
        
        if (PlayerPrefs.GetInt("IsPurchased", 0) == 0)
        {
            AppLovin.SetSdkKey("Ue9JOxMHwDTavooHX8KiEwE7ITMCUVTz9cHTTdrTsI2m5q1cyMKBHTIZ5fnZAEXJsqsORhtI-w24k4hgQ0shZ9");
            AppLovin.InitializeSdk();

            GetComponent<GoogleMobileAdsDemoScript>().RequestBanner();
        }
        

        if (PlayerPrefs.GetInt("SoundOnOff", 1) == 1)
        {
            GetComponent<AudioSource>().clip = audios[UnityEngine.Random.Range(0, 5)];
            GetComponent<AudioSource>().Play();
        }

    }


    public void PlayRandomMusic()
    {
        if (PlayerPrefs.GetInt("SoundOnOff", 1) == 1)
        {
            
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = audios[UnityEngine.Random.Range(0, 5)];
            GetComponent<AudioSource>().Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
