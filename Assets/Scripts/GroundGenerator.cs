using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{

    public GameObject thePlatform;
    public GameObject[] thePlatforms;
    //public GameObject[] theHalfPlatforms;
    public GameObject[] rings;
    public GameObject stars;

    public GameObject colorChangeCube;

    public Transform generationPoint;
    public float distanceBetween = 19.0f;
    public float platformWidth;
    private int platformSelectorLeft;
    private int platformSelector;
    private int platformSelectorRight;

    bool colorNotMatch;

    int[] currentArray = new int[3];
    int[] previousArray = new int[3];

    float starRandomThreshold = 50f;


    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider>().bounds.size.z;
        platformWidth = thePlatform.transform.localScale.z;

        previousArray[0] = 0;
        previousArray[1] = 1;
        previousArray[2] = 2;
    }

    void Update()
    {
        if (MenuController.play)
        {
            if (generationPoint)
            {
                if (transform.position.z < generationPoint.transform.position.z)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + platformWidth + distanceBetween);

                    colorNotMatch = false;

                    platformSelectorLeft = Random.Range(0, thePlatforms.Length);

                    platformSelector = Random.Range(0, thePlatforms.Length);
                    while (platformSelector == platformSelectorLeft)
                    {
                        platformSelector = Random.Range(0, thePlatforms.Length);
                    }

                    platformSelectorRight = Random.Range(0, thePlatforms.Length);
                    while (platformSelectorRight == platformSelectorLeft || platformSelectorRight == platformSelector)
                    {
                        platformSelectorRight = Random.Range(0, thePlatforms.Length);
                    }

                    currentArray[0] = platformSelectorLeft;
                    currentArray[1] = platformSelector;
                    currentArray[2] = platformSelectorRight;

                    if (currentArray[2] == previousArray[0] || currentArray[0] == previousArray[2])
                    {
                        colorNotMatch = false;
                    }
                    else
                    {
                        colorNotMatch = true;
                    }



                    while (colorNotMatch == false)
                    {
                        //if (currentArray[2] == previousArray[0]) {
                        platformSelectorLeft = Random.Range(0, thePlatforms.Length);

                        platformSelector = Random.Range(0, thePlatforms.Length);
                        while (platformSelector == platformSelectorLeft)
                        {
                            platformSelector = Random.Range(0, thePlatforms.Length);
                        }

                        platformSelectorRight = Random.Range(0, thePlatforms.Length);
                        while (platformSelectorRight == platformSelectorLeft || platformSelectorRight == platformSelector)
                        {
                            platformSelectorRight = Random.Range(0, thePlatforms.Length);
                        }

                        currentArray[0] = platformSelectorLeft;
                        currentArray[1] = platformSelector;
                        currentArray[2] = platformSelectorRight;
                        //} else {
                        if (currentArray[2] != previousArray[0] && currentArray[0] != previousArray[2])
                        {
                            colorNotMatch = true;
                        }
                    }



                    Instantiate(thePlatforms[platformSelectorLeft], new Vector3(transform.position.x - 2f,                  //platform generation           						transform.position.y + 1.45f,
                                                        transform.position.y,
                                                            transform.position.z),
                                                                transform.rotation);

                    if (Random.Range(0f, 100f) < starRandomThreshold)
                    {
                        Instantiate(stars, new Vector3(transform.position.x - 2f,                            // star generation
                                                   transform.position.y + 1f,
                                                       transform.position.z), transform.rotation);
                    }



                    //thePlatforms[platformSelector].gameObject.transform.localScale = new Vector3((0, 0, 6f);
                    GameObject g = Instantiate(thePlatforms[platformSelector], new Vector3(transform.position.x,                           //platform generation           						transform.position.y + 1.45f,
                                                            transform.position.y,
                                                            transform.position.z - 2f),
                                                            transform.rotation);
                    g.transform.localScale = new Vector3(2, 1, 8f);

                    Instantiate(colorChangeCube, new Vector3(transform.position.x,                           //white tile generation for color change          						transform.position.y + 1.45f,
                                                             transform.position.y,
                                                             transform.position.z + 4f),
                                                             transform.rotation);


                    if (Random.Range(0f, 100f) < starRandomThreshold)
                    {
                        Instantiate(stars, new Vector3(transform.position.x,                            // star generation
                                                   transform.position.y + 1f,
                                                       transform.position.z), transform.rotation);
                    }

                    Instantiate(thePlatforms[platformSelectorRight], new Vector3(transform.position.x + 2f,                 //platform generation           						transform.position.y + 1.45f,
                                                        transform.position.y,
                                                            transform.position.z),
                                                                transform.rotation);


                    if (Random.Range(0f, 100f) < starRandomThreshold)
                    {
                        Instantiate(stars, new Vector3(transform.position.x + 2f,                            // star generation
                                                   transform.position.y + 1f,
                                                       transform.position.z), transform.rotation);
                    }


                    for (int i = 0; i < thePlatforms.Length; i++)
                    {
                        previousArray[i] = currentArray[i];
                    }

                }
            }
        }
    }
}

