using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChange : MonoBehaviour
{ 
    public Renderer rend;
    public float spawnTime = 1.0f;

    //int ballColorChangeCount;                         // hopw many shot after ball will change color

    public Material[] myMaterials = new Material[3];
    System.Random random = new System.Random();         // for random color generation

    public static int score=0;
    public static bool shouldDestroy;                   // if touch another color destroy gameobject

    void Start() {
        rend = GetComponent<Renderer>();

        //GameObject[] lightCircle;
        //lightCircle = GameObject.FindGameObjectsWithTag("Ring-Inside");


        InvokeRepeating("TimeGapForColorChange", 0.0f, spawnTime);
        //child = GetComponentInChildren<Renderer>().material;

        //gameObject.GetComponent<Renderer>().material.color = GetComponentInChildren<Renderer>().material.color; 
        //myMaterials[random.Next(0, myMaterials.Length)];
    }



    void TimeGapForColorChange()
    {
        //GetComponentInChildren<Renderer>().material.color = myMaterials[random.Next(0, myMaterials.Length)].color;
        GetComponent<Renderer>().material.color = myMaterials[random.Next(0, myMaterials.Length)].color;

    }


    void OnTriggerEnter(Collider col) {


      /*  if (col.gameObject.tag == "Player") {
            if (col.gameObject.GetComponent<Renderer>().material.color == rend.material.color) {
                hit.Play();
                score++;
                Debug.Log("SCORE: " + score);
                Debug.Log("SAME COLOR CIRCLE");

            }
            if (col.gameObject.GetComponent<Renderer>().material.color != rend.material.color) {
                score = 0;
                notHit.Play();
                Debug.Log("NOT COLOR CIRCLE");
                Application.LoadLevel(Application.loadedLevel);
            }*/
       // }
    }
}

