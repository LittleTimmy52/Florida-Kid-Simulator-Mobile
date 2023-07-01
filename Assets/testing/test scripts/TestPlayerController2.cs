using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPlayerController2 : MonoBehaviour
{
    private float TPYVal;
    private float testHorizontalInput;
    private float testVerticalInput;
    //public float testSpeed = 800;
    public float testSpeed2 = 10;
    private Rigidbody testPlayerRb;
    private float testXBound = 50;
    private float testZBound = 50;
    public float testJumpForce = 500;
    public bool testIsOnGround;
    private GameObject testGameManager;
    private TestSpawnManager4 TSM4;
    public bool testIsGamePaused;
    public GameObject testPauseMenu;
    private TestGameManager TGM;
    private bool testGameOver;
    public GameObject testPCam1st;
    public GameObject testPCam3rd;
    private bool test3rdPersonToggle;

    // Start is called before the first frame update
    void Start()
    {
        testPlayerRb = GetComponent<Rigidbody>();
        testGameManager = GameObject.Find("Test Game Manager");
        TSM4 = testGameManager.GetComponent<TestSpawnManager4>();
        TGM = testGameManager.GetComponent<TestGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            test3rdPersonToggle = !test3rdPersonToggle;
        }

        if (test3rdPersonToggle == true)
        {
            testPCam1st.SetActive(false);
            testPCam3rd.SetActive(true);
        }else
        {
            testPCam1st.SetActive(true);
            testPCam3rd.SetActive(false);
        }

        testGameOver = testGameManager.GetComponent<TestGameManager>().testIsGameOver;
        if (testGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                testIsGamePaused = !testIsGamePaused;
            }
        }
        
        if (testIsGamePaused == true && testGameOver == false)
        {
            testPauseMenu.SetActive(true);
            Time.timeScale = 0;
        }else
        {
            testPauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        if (testGameOver == true)
        {
            Time.timeScale = 0f;
            testPauseMenu.SetActive(false);
        }

        testHorizontalInput = Input.GetAxis("Horizontal");
        testVerticalInput = Input.GetAxis("Vertical");

        #region  TEST STUFF

        //testPlayerRb.AddForce(Vector3.right * testHorizontalInput * Time.deltaTime * testSpeed);
        //testPlayerRb.AddForce(Vector3.forward * testVerticalInput * Time.deltaTime * testSpeed);
        
        /*if (Input.GetKey(KeyCode.M))
        {
            testPlayerRb.AddForce(Vector3.right * testHorizontalInput * Time.deltaTime * testSpeed * 10);
            testPlayerRb.AddForce(Vector3.forward * testVerticalInput * Time.deltaTime * testSpeed * 10);
        } else if (Input.GetKey(KeyCode.N))
        {
            testPlayerRb.AddForce(Vector3.right * testHorizontalInput * Time.deltaTime * testSpeed * 50);
            testPlayerRb.AddForce(Vector3.forward * testVerticalInput * Time.deltaTime * testSpeed * 50);
        } else
        {
            testPlayerRb.AddForce(Vector3.right * testHorizontalInput * Time.deltaTime * testSpeed);
            testPlayerRb.AddForce(Vector3.forward * testVerticalInput * Time.deltaTime * testSpeed);
        }*/

        
        if (Input.GetKey(KeyCode.M))
        {
            transform.Translate(Vector3.right * testHorizontalInput * Time.deltaTime * testSpeed2 * 10);
            transform.Translate(Vector3.forward * testVerticalInput * Time.deltaTime * testSpeed2 * 10);
        } else
        {
            transform.Translate(Vector3.right * testHorizontalInput * Time.deltaTime * testSpeed2);
            transform.Translate(Vector3.forward * testVerticalInput * Time.deltaTime * testSpeed2);
        }

        if (Input.GetKeyDown(KeyCode.Space) && testIsOnGround == true)
        {
            testPlayerRb.AddForce(Vector3.up * testJumpForce);
            testIsOnGround = false;
        }
                #endregion


        transform.rotation = Quaternion.Euler(0, TPYVal, 0);

        if (Input.GetKey(KeyCode.Q) && testIsGamePaused == false && testGameOver == false)
        {
            TPYVal -= 1;
        }
        if (Input.GetKey(KeyCode.E) && testIsGamePaused == false && testGameOver == false)
        {
            TPYVal += 1;
        }

        // the bounds on the x axis
        if (transform.position.x < -testXBound)
        {
            transform.position = new Vector3(-testXBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > testXBound)
        {
            transform.position = new Vector3(testXBound, transform.position.y, transform.position.z);
        }

        // the bounds on the y axis
        if (transform.position.z < -testZBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -testZBound);
        }
        if (transform.position.z > testZBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, testZBound);
        }

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
            Debug.Log("player clipped, there velocity is " + testPlayerRb.velocity);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Player velocity is " + testPlayerRb.velocity);
        }

        /*if (testPlayerRb.velocity.y < -10)
        {
            testPlayerRb.velocity = new Vector3(testPlayerRb.velocity.x, 0, testPlayerRb.velocity.z);
            Debug.Log("Yvelocity canceld its now " + testPlayerRb.velocity);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            testIsOnGround = true;
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            Debug.Log("colided with Flag");
            Destroy(other.gameObject);
            //TSM4.PositionRaycastFlag();
            testFlagCount += 1;
            TGM.GetComponent<TestSpawnManager7>().spawnFlag = true;
            TGM.GetComponent<TestSpawnManager7>().PosRaycast();
        }

        if (other.CompareTag("Cop"))
        {
            Debug.Log("collided with enemy");
            TGM.testIsGameOver = true;
        }
    }

    private int _flagCount;
    public int testFlagCount
    {
        get => _flagCount;
        private set
        {
            _flagCount = value;
            if(_flagCount % 5 == 0)
            {
                testGameManager.GetComponent<TestSpawnManager7>().spawnCop = true;
                testGameManager.GetComponent<TestSpawnManager7>().PosRaycast();
                Debug.Log("u got 5 flags");
                TGM.TestUpdateEnemyCount(1);
            }
        }
    }
}
// max speed without clip is 15 (not add force)