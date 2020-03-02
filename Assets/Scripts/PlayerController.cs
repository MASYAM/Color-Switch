using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float ballForwardSpeed;
    public float ballJumpSpeed;

    public int valueLeftRightLane = 0;                             // for limit left or right movement
    public int leftRightMovementValue = 6;
    bool leftOrRightTouch;

    Rigidbody myRigidbody;
    public Material[] myMaterials = new Material[3];
    System.Random random = new System.Random();

    public ParticleSystem afterDeathRed;
    public ParticleSystem afterDeathGreen;
    public ParticleSystem afterDeathBlue;

    public Renderer rend;

    public AudioSource[] ballTouchSounds;
    public AudioSource hit;
    public AudioSource notHit;
    public AudioSource hitCircle;
    public AudioSource jumpSound;
    public AudioSource starSound;

    public static int pointScore = 0;
    public static int starScore = 0;

    public bool canJump = false;

    public float speedMultiplier;
    public float speedIncreaseMileStone;
    private float speedMileStoneCount;


    //private float fingerStartTime = 0.0f;
    //private Vector2 fingerStartPos = Vector2.zero;

    //private bool isSwipe = false;
    //private float minSwipeDist = 5.0f;
    //private float maxSwipeTime = 1.0f;

    private static int numOfPlay = 0;
    public static int bestPoint, bestStars;



    void Start()
    {
        //PlayerPrefs.DeleteAll();
        GameObject.Find("DontDestroy").GetComponent<AudioSource>().volume = 1.0f;

        Physics.gravity = new Vector3(0, -55f, 0);               //reset the gravity of the ball to -30
        pointScore = 0;
        starScore = 0;
        bestPoint = PlayerPrefs.GetInt("HighPoint");
        bestStars = PlayerPrefs.GetInt("HighStar");

        rend = GetComponent<Renderer>();
        myRigidbody = GetComponent<Rigidbody>();
        gameObject.GetComponent<Renderer>().material.color = myMaterials[random.Next(0, myMaterials.Length)].color;

        ballTouchSounds = GetComponents<AudioSource>();
        hit = ballTouchSounds[0];
        notHit = ballTouchSounds[1];
        hitCircle = ballTouchSounds[2];
        jumpSound = ballTouchSounds[3];
        starSound = ballTouchSounds[4];

        //Ad
        numOfPlay++;

        if (numOfPlay % 4 == 0)
        {
            if (PlayerPrefs.GetInt("IsPurchased", 0) == 0)
            {
                AppLovin.PreloadInterstitial();
            }
        }

    }


    //Vector2 firstPressPos;
    //Vector2 secondPressPos;
    //Vector2 currentSwipe;

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
    private bool isSwipe = false;
    private float minSwipeDist = 1.0f;
    private float maxSwipeTime = 0.5f;

    bool jumpPressed;
    string previousLane = "middle";

    void FixedUpdate()
    {
        if (jumpPressed)
        {
            jumpSound.Play();
            myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
            canJump = false;
            jumpPressed = false;
        }
    }


    void Update()
    {

        if (MenuController.play)
        {
            leftRightMovementValue = 6;
            if (transform.position.z > speedMileStoneCount)
            {
                speedMileStoneCount += speedIncreaseMileStone;
                ballForwardSpeed *= speedMultiplier;
            }

            transform.Translate(Vector3.forward * ballForwardSpeed * Time.deltaTime);
            /*

            #if UNITY_EDITOR
                        if (Input.GetKeyDown("up") && canJump) {
                            jumpPressed = true;
                        }

                        if (Input.GetKeyDown("left") && leftOrRightTouch == false && valueLeftRightLane > -1 && canJump) {
                            myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                            canJump = false;

                            valueLeftRightLane -= 1;

                            myRigidbody.velocity = new Vector3(-leftRightMovementValue, 0, 0);
                            leftOrRightTouch = true;
                            StartCoroutine(stopSliding());
                        }
                        if (Input.GetKeyDown("right") && leftOrRightTouch == false && valueLeftRightLane < 1 && canJump) {
                            myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                            canJump = false;

                            valueLeftRightLane += 1;

                            myRigidbody.velocity = new Vector3(leftRightMovementValue, 0, 0);
                            leftOrRightTouch = true;
                            StartCoroutine(stopSliding());
                        }

            #elif UNITY_ANDROID || UNITY_IOS
            */



            if (Input.GetMouseButtonDown(0) && (Input.mousePosition.x < Screen.width / 3) && leftOrRightTouch == false && (valueLeftRightLane > -1 || previousLane == "left") && canJump)
            {

                //Left
                if (previousLane == "left")
                {
                    jumpPressed = true;
                    //valueLeftRightLane -= 1;
                    //leftOrRightTouch = true;
                    previousLane = "left";
                }
                else if (previousLane == "middle")
                {
                    myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                    canJump = false;

                    valueLeftRightLane -= 1;

                    myRigidbody.velocity = new Vector3(-leftRightMovementValue, 0, 0);
                    leftOrRightTouch = true;
                    previousLane = "left";
                }
                else
                {
                    //that means ball on right
                    myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                    canJump = false;

                    valueLeftRightLane -= 1;

                    myRigidbody.velocity = new Vector3(-leftRightMovementValue, 0, 0);
                    leftOrRightTouch = true;
                    previousLane = "middle"; // ball will move to middle so set middle
                }


                StartCoroutine(stopSliding());

            }
            else if (Input.GetMouseButtonDown(0) && (Input.mousePosition.x >= Screen.width / 3) && (Input.mousePosition.x <= (Screen.width / 3) * 2) && canJump)
            {
                //Middle/up
                if (previousLane == "middle")
                {
                    jumpPressed = true;
                }

                if (previousLane == "left")
                {
                    //move to right
                    myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                    canJump = false;

                    valueLeftRightLane += 1;

                    myRigidbody.velocity = new Vector3(leftRightMovementValue, 0, 0);
                    leftOrRightTouch = true;
                    StartCoroutine(stopSliding());
                }
                else if (previousLane == "right")
                {
                    //move to left
                    myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                    canJump = false;

                    valueLeftRightLane -= 1;

                    myRigidbody.velocity = new Vector3(-leftRightMovementValue, 0, 0);
                    leftOrRightTouch = true;
                    StartCoroutine(stopSliding());
                }


                previousLane = "middle";


            }
            else if (Input.GetMouseButtonDown(0) && (Input.mousePosition.x > (Screen.width / 3) * 2) && (Input.mousePosition.x <= Screen.width) && leftOrRightTouch == false && (valueLeftRightLane < 1 || previousLane == "right") && canJump)
            {
                //Right
                if (previousLane == "right")
                {
                    jumpPressed = true;
                    // valueLeftRightLane += 1;
                    //leftOrRightTouch = true;
                    previousLane = "right";
                }
                else if (previousLane == "middle")
                {
                    myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                    canJump = false;

                    valueLeftRightLane += 1;

                    myRigidbody.velocity = new Vector3(leftRightMovementValue, 0, 0);
                    leftOrRightTouch = true;
                    previousLane = "right";
                }
                else
                {
                    //that means ball on left
                    myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                    canJump = false;

                    valueLeftRightLane += 1;

                    myRigidbody.velocity = new Vector3(leftRightMovementValue, 0, 0);
                    leftOrRightTouch = true;
                    previousLane = "middle"; //ball will move to middle so set it middle

                }


                StartCoroutine(stopSliding());

            }

            /*
                        var dir = Vector3.zero;
                        if (Input.touchCount > 0){

                        foreach (Touch touch in Input.touches) {
                            switch (touch.phase) {
                            case TouchPhase.Began :



                                isSwipe = true;
                                fingerStartTime = Time.time;
                                fingerStartPos = touch.position;
                                break;

                            case TouchPhase.Canceled :


                                isSwipe = false;
                                break;

                            case TouchPhase.Ended :

                                float gestureTime = Time.time - fingerStartTime;
                                float gestureDist = (touch.position - fingerStartPos).magnitude;

                                if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
                                    Vector2 direction = touch.position - fingerStartPos;
                                    Vector2 swipeType = Vector2.zero;

                                    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
                                        // the swipe is horizontal:
                                        swipeType = Vector2.right * Mathf.Sign(direction.x);
                                    }else{
                                        // the swipe is vertical:
                                        swipeType = Vector2.up * Mathf.Sign(direction.y);
                                    }

                                    if(swipeType.x != 0.0f){
                                        if(swipeType.x > 0.0f){
                                            // MOVE RIGHT

                                            if (leftOrRightTouch == false && valueLeftRightLane < 1 && canJump)
                                            {

                                                myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                                                canJump = false;

                                                valueLeftRightLane += 1;

                                                myRigidbody.velocity = new Vector3(leftRightMovementValue, 0, 0);
                                                leftOrRightTouch = true;
                                                StartCoroutine(stopSliding());
                                            }

                                          }
                                            else{
                                            // MOVE LEFT
                                            if (leftOrRightTouch == false && valueLeftRightLane > -1 && canJump)
                                            {

                                                myRigidbody.AddForce(Vector3.up * ballJumpSpeed, ForceMode.VelocityChange);
                                                canJump = false;

                                                valueLeftRightLane -= 1;

                                                myRigidbody.velocity = new Vector3(-leftRightMovementValue, 0, 0);
                                                leftOrRightTouch = true;
                                                StartCoroutine(stopSliding());
                                            }


                                        }
                                    }

                                    if(swipeType.y != 0.0f ){
                                        if(swipeType.y > 0.0f){
                                            // MOVE UP
                                            if (canJump)
                                            {
                                                jumpPressed = true;
                                            }

                                        }else{
                                            // MOVE DOWN

                                        }
                                    }

                                }

                                break;
                            }
                        } 
                    }*/

            //#endif


        }
    }


    /*public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    if (canJump)
                    {
                        jumpSound.Play();
                        myRigidbody.AddForce(Vector3.up * ballJumpSpeed);
                        //transform.Translate(Vector3.up * ballJumpSpeed * Time.deltaTime);
                        canJump = false;
                    }
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (leftOrRightTouch == false && valueLeftRightLane > -1 && canJump)
                    {
                        myRigidbody.AddForce(Vector3.up * ballJumpSpeed);
                        canJump = false;

                        valueLeftRightLane -= 1;
                        myRigidbody.velocity = new Vector3(-leftRightMovementValue, 0, 0);
                        leftOrRightTouch = true;
                        StartCoroutine(stopSliding());
                    }
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (leftOrRightTouch == false && valueLeftRightLane < 1 && canJump)
                    {
                        myRigidbody.AddForce(Vector3.up * ballJumpSpeed);
                        canJump = false;

                        valueLeftRightLane += 1;
                        myRigidbody.velocity = new Vector3(leftRightMovementValue, 0, 0);
                        leftOrRightTouch = true;
                        StartCoroutine(stopSliding());
                    }
                }
            }
        }
    }
    */


    public GameObject particleStar;
    Color color;


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Coin")
        {
            starScore++;
            starSound.Play();
            Instantiate(particleStar, new Vector3(transform.position.x,                     // star particle
                                                    transform.position.y,
                                                        transform.position.z), transform.rotation);

            if (PlayerPrefs.GetInt("HighStar") < starScore)
            {
#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                GameObject.Find("DontDestroy").GetComponent<IosLeaderBoardAccess>().OnPostScore((long)starScore, 2);
#endif
                PlayerPrefs.SetInt("HighStar", starScore);
                bestStars = starScore;

            }

            Destroy(col.gameObject);

        }

        if (col.gameObject.tag == "Ring-Inside")
        {
            if (col.gameObject.GetComponent<Renderer>().material.color == rend.material.color)
            {
                hit.Play();
                pointScore++;
                //Debug.Log("SCORE: " + score);
                //Debug.Log("SAME COLOR CIRCLE");
                if (PlayerPrefs.GetInt("HighPoint") < pointScore)
                {
#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                    GameObject.Find("DontDestroy").GetComponent<IosLeaderBoardAccess>().OnPostScore((long)pointScore, 1);
#endif
                    PlayerPrefs.SetInt("HighPoint", pointScore);
                    bestPoint = pointScore;

                }



            }

            if (col.gameObject.GetComponent<Renderer>().material.color != rend.material.color)
            {
                pointScore = 0;
                notHit.Play();
                //Debug.Log("NOT COLOR CIRCLE");
                StartCoroutine(waitAfterDead());
                print("test running");
            }

        }
    }





    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Color-Change-Cube")
        {
            color = myMaterials[random.Next(0, myMaterials.Length)].color;
            while (color == gameObject.GetComponent<Renderer>().material.color)
            {
                color = myMaterials[random.Next(0, myMaterials.Length)].color;
            }
            gameObject.GetComponent<Renderer>().material.color = color;

            canJump = true;

        }

        if (other.gameObject.tag == "Circle")
        {

            hitCircle.Play();
            //particles
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("CIRCLE DESTROY");
            StartCoroutine(waitAfterDead());
        }

        if (other.gameObject.tag == "Floor")
        {
            canJump = true;
        }

        if (other.gameObject.tag == "Death-Quad")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("CIRCLE DESTROY");
            StartCoroutine(waitAfterDead());
        }

    }





    IEnumerator waitAfterDead()
    {

        ballForwardSpeed = 0;
        ballJumpSpeed = 0;

        if (gameObject.GetComponent<Renderer>().material.color == myMaterials[0].color)
        {
            Instantiate(afterDeathRed, new Vector3(transform.position.x,
                                                     transform.position.y + .5f,
                                                         transform.position.z), transform.rotation);
        }
        if (gameObject.GetComponent<Renderer>().material.color == myMaterials[1].color)
        {
            Instantiate(afterDeathGreen, new Vector3(transform.position.x,
                                                     transform.position.y + .5f,
                                                         transform.position.z), transform.rotation);
        }
        if (gameObject.GetComponent<Renderer>().material.color == myMaterials[2].color)
        {
            Instantiate(afterDeathBlue, new Vector3(transform.position.x,
                                                     transform.position.y + .5f,
                                                         transform.position.z), transform.rotation);
        }



        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Start");

        //ad
        if (numOfPlay % 4 == 0)
        {
            if (PlayerPrefs.GetInt("IsPurchased", 0) == 0)
            {
                if (AppLovin.HasPreloadedInterstitial())
                {
                    AppLovin.ShowInterstitial();
                }
            }
        }

        GameObject.Find("DontDestroy").GetComponent<AudioSource>().volume = 0.8f;
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("DontDestroy").GetComponent<AudioSource>().volume = 0.5f;
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("DontDestroy").GetComponent<AudioSource>().volume = 0.3f;
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("DontDestroy").GetComponent<AudioSource>().volume = 0.05f;
        GameObject.Find("DontDestroy").GetComponent<SetDontDestroyOnLoad>().PlayRandomMusic();

        yield return new WaitForSeconds(1.0f);
        Debug.Log("end");

        GoToRandomStage();
    }

    IEnumerator stopSliding()
    {
        yield return new WaitForSeconds(0.3f);
        leftRightMovementValue = 0;
        leftOrRightTouch = false;
        myRigidbody.velocity = new Vector3(0, 0, 0);
        myRigidbody.angularVelocity = new Vector3(0, 0, 0);

    }


    void GoToRandomStage()
    {

        SceneManager.LoadScene(UnityEngine.Random.Range(0, 5));
    }

}


