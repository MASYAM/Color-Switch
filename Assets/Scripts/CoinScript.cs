using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public float rotateSpeed = 5.0f;
    public  float spawnTime = 2.0f;
    System.Random random = new System.Random();         // for random color generation

    public Material[] myMaterials = new Material[3];

    void Start() {
        //InvokeRepeating("TimeGapForColorChange", 0.0f, spawnTime);
        //GetComponentInChildren<Renderer>().material.color = myMaterials[random.Next(0, myMaterials.Length)].color;
    }

    void Update () {

        transform.Rotate(new Vector3(0, 30, 0) * rotateSpeed * Time.deltaTime);                 // star rotation
        //transform.Rotate(new Vector3(15, 30, 45) * rotateSpeed * Time.deltaTime);             // for box rotation
        //gameObject.GetComponent<Renderer>().material = myMaterials[random.Next(0, myMaterials.Length)];
    }

    void TimeGapForColorChange() {
        GetComponentInChildren<Renderer>().material.color = myMaterials[random.Next(0, myMaterials.Length)].color;
        //GetComponent<Renderer>().material.color = myMaterials[random.Next(0, myMaterials.Length)].color;
        
    }


}
