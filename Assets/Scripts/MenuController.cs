using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    Animator menuController;
    public static bool play;
    AudioSource playButtonMusic;

    public GameObject[] allButtonObjects;


    // Use this for initialization
    void Start() {
        menuController = GetComponent<Animator>();
        playButtonMusic = GetComponent<AudioSource>();
        menuController.Play("AllButtonsAnim");
        play = false;
    }

    public void Play() {
        menuController.Play("AllButtonsAnimRev");
        play = true;
        playButtonMusic.Play();

        StartCoroutine(WaitForAnimationEnd(1.0f));
    
    }

    IEnumerator WaitForAnimationEnd(float waitingTime) {
        yield return new WaitForSeconds(waitingTime);
        for (int i = 0; i < allButtonObjects.Length; i++)
        {
            allButtonObjects[i].SetActive(false);
        }
    }
}
