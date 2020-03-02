using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareAndRateUs : MonoBehaviour {


    public Animator TutorialAnim;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt("IsFirstTimePlay",0) == 0)
        {
            StartCoroutine("ForFirstTimePlay");
            PlayerPrefs.SetInt("IsFirstTimePlay", 1);
        }
		
	}

    public void CrazyRush()
    {

#if UNITY_ANDROID
          Application.OpenURL("market://details?id=com.pixitalegames.bottlefliprocket");
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
		  Application.OpenURL("itms-apps://itunes.apple.com/app/id1209819699");
#endif
    }


    public void Insect()
    {
#if UNITY_ANDROID
                Application.OpenURL("market://details?id=com.pixitalegames.gameofinsect");
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
		        Application.OpenURL("itms-apps://itunes.apple.com/app/id1164188043");
#endif
    }


    public void Bottle()
    {
#if UNITY_ANDROID
               Application.OpenURL("market://details?id=com.pixitalegames.bottlefliprocket");
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
		        Application.OpenURL("itms-apps://itunes.apple.com/app/id1217817273");
#endif
    }

    public void Rate()
    {
#if UNITY_ANDROID
                    Application.OpenURL("market://details?id=com.pixitalegames.bottlefliprocket");
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
		           Application.OpenURL("itms-apps://itunes.apple.com/app/id1262735873");
#endif
    }


    public void TutorialAnimation()
    {
        if (TutorialAnim.GetBool("ShouldGo"))
        {
            TutorialAnim.SetBool("ShouldGo", false);
        }
        else {
            TutorialAnim.SetBool("ShouldGo", true);
        }
        GetComponent<Button>().enabled = false;
        StartCoroutine("ForAvoidingDoubleClick");
    }


    IEnumerator ForAvoidingDoubleClick()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Button>().enabled = true;
    }


    IEnumerator ForFirstTimePlay()
    {
        yield return new WaitForSeconds(1.0f);
        TutorialAnimation();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
