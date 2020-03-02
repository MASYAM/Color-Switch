using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerObject : MonoBehaviour
{
    public GameObject destructionPoint;
    public float distanceBetween;

    void Start() {
        destructionPoint = GameObject.Find("DestructionPoint");
    }

    void Update() {
        if (destructionPoint) {
            if (transform.position.z < destructionPoint.transform.position.z) {
                Destroy(gameObject);
            }
        }
    }
}



