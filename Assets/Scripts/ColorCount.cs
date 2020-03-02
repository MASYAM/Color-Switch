using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorCount : MonoBehaviour {

    void Start() {
        //gameObject.GetComponent<Renderer>().material.color = own.transform.Find("Coin").gameObject.GetComponent<Renderer>().material.color;
         gameObject.GetComponent<Renderer>().material.color = GetComponentInChildren<Renderer>().material.color;    
    }
}


