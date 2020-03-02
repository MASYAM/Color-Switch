using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    void LateUpdate() {
        if (player) {
            this.transform.position = new Vector3(/*target.transform.position.x + */xOffset,
                                                /*target.transform.position.y +*/ yOffset,
                                                player.transform.position.z + zOffset);
        }
    }
}

