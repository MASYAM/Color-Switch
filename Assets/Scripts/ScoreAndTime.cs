using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndTime : MonoBehaviour {

    public Text pointText;
    public Text starText;
    public Text BestPointText;
    public Text BestStarText;
    //public Text highScoreText;
    //public float time;
    //public bool timeStarted;

    private void Start() {
        //timeStarted = true;
    }

    void Update() {

        /*if (timeStarted == true) {
            time += Time.deltaTime;
        }

        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        */

        pointText.text = "POINT " + PlayerController.pointScore.ToString();
        starText.text = PlayerController.starScore.ToString() + " STARS";
        BestPointText.text = "BEST " + PlayerController.bestPoint.ToString();
        BestStarText.text = PlayerController.bestStars.ToString() + " BEST";
    }
}



