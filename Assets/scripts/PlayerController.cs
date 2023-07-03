using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    #region PRIVATE

    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;
    private float f1Input;
    private float f2Input;
    private float f3Input;
    private float speed = 10f;
    private float xBound = 50f;
    private float zBound = 50f;
    private float jumpForce = 500f;
    private bool isOnGround;
    private int skinSelected;
    private Rigidbody playerRb;
    private GameManager gM;
    private SpawnManager sM;
    private Animator animP1;
    private Animator animP2;
    private Animator animP3;
    private Animator animP4;

    private Vector2 movDir = Vector2.zero;
    private GetInp inp;
    private InputAction move;
    private InputAction jump;
    private InputAction pause;
    private InputAction pov;
    private int sel = 1;

    #endregion

    #region  PUBLIC

    [Header("Player cmaeras")]
    public GameObject firstPersonCam;
    public GameObject secondPersonCam;
    public GameObject thirdPersonCam;

    [Header("Skins")]
    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    public GameObject skin4;

    [Header("First pause button")]
    public GameObject pauseFirstButton;

    [Header("Other")]
    public GameObject gMObj;
    public GameObject pauseMenu;
    public GameObject crosshairs;
    public int difficulty = 5;
    public bool isGamePaused;

    #endregion

    private void Awake()
    {
        inp = new GetInp();
    }

    private void OnEnable()
    {
        // Really all this is needed for is so the Input system doesent yell at me
        move = inp.Player.Move;
        jump = inp.Player.Jump;
        pause = inp.Player.Pause;
        pov = inp.Player.Pov;

        move.Enable();
        jump.Enable();
        pause.Enable();
        pov.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // sets the rigidbody
        playerRb = GetComponent<Rigidbody>();

        // sets script refrences
        gM = gMObj.GetComponent<GameManager>();
        sM = gMObj.GetComponent<SpawnManager>();

        // sets the difficulty
        difficulty = PlayerPrefs.GetInt("Difficulty");

        // sets the skin
        skinSelected = PlayerPrefs.GetInt("SkinActive");

        // sets the anim
        animP1 = skin1.GetComponent<Animator>();
        animP2 = skin2.GetComponent<Animator>();
        animP3 = skin3.GetComponent<Animator>();
        animP4 = skin4.GetComponent<Animator>();

        #region SELECT SKIN

        // depending on shat is selected it would change the skin
        if(skinSelected == 1)
        {
            skin1.SetActive(true);
            skin2.SetActive(false);
            skin3.SetActive(false);
            skin4.SetActive(false);
        }

        if(skinSelected == 2)
        {
            skin1.SetActive(false);
            skin2.SetActive(true);
            skin3.SetActive(false);
            skin4.SetActive(false);
        }

        if(skinSelected == 3)
        {
            skin1.SetActive(false);
            skin2.SetActive(false);
            skin3.SetActive(true);
            skin4.SetActive(false);
        }

        if(skinSelected == 4)
        {
            skin1.SetActive(false);
            skin2.SetActive(false);
            skin3.SetActive(false);
            skin4.SetActive(true);
        }

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        movDir = move.ReadValue<Vector2>();
        Bounds();

        if (pause.WasPressedThisFrame())
            {
                print("pressed");
                if (gM.isGameOver == false)
                {
                    print("pause toggle");
                    isGamePaused = !isGamePaused;
                    Pause();
                }
            }

        #region  CAM CHANGE

        if (gM.isGameActive == true)
        {
            // changes the cam to the 1st person cam
            if (sel == 1)
            {
                firstPersonCam.SetActive(true);
                secondPersonCam.SetActive(false);
                thirdPersonCam.SetActive(false);
            }

            // changes the cam to the 2nd person cam
            if (sel == 2)
            {
                firstPersonCam.SetActive(false);
                secondPersonCam.SetActive(true);
                thirdPersonCam.SetActive(false);
            }

            // changes the cam to the 3rd person cam
            if (sel == 3)
            {
                firstPersonCam.SetActive(false);
                secondPersonCam.SetActive(false);
                thirdPersonCam.SetActive(true);
            }

            if (pov.WasPressedThisFrame())
            {
                if (sel < 3)
                {
                    sel ++;
                }else
                {
                    sel = 1;
                }
            }

            #endregion

            if(transform.position.y < -20)
            {
                transform.position = new Vector3(0, 2.2f, 0);
            }

            #region ANIMATION TRIGGERS

            if(movDir.x != 0 || movDir.y != 0)
            {
                if(skin1.activeInHierarchy == true)
                {
                    animP1.SetTrigger("running");
                }
                if(skin2.activeInHierarchy == true)
                {
                    animP2.SetTrigger("running");
                }
                if(skin3.activeInHierarchy == true)
                {
                    animP3.SetTrigger("running");
                }
                if(skin4.activeInHierarchy == true)
                {
                    animP4.SetTrigger("running");
                }
            }

            if(movDir.x == 0 && movDir.y == 0)
            {
                if(skin1.activeInHierarchy == true)
                {
                    animP1.SetTrigger("idle");
                }
                if(skin2.activeInHierarchy == true)
                {
                    animP2.SetTrigger("idle");
                }
                if(skin3.activeInHierarchy == true)
                {
                    animP3.SetTrigger("idle");
                }
                if(skin4.activeInHierarchy == true)
                {
                    animP4.SetTrigger("idle");
                }
            }
        }

        #endregion
    }

    private void FixedUpdate()
    {
        // fixes collision due to speed
        if(movDir.x != 0 && movDir.y != 0)
        {
            speed = 8.5f;
        }else
        {
            speed = 10f;
        }

        // moves the player
        transform.Translate(Vector3.right * movDir.x * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * movDir.y * Time.deltaTime * speed);

        //Jumping
        if (jump.WasPressedThisFrame() && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce);
            isOnGround = false;
        }
    }

    private void Bounds()
    {
        // Keep player within the bounds of the level
        float boundBox = 50f;

        Vector3 pos = transform.position;

        if (pos.x > boundBox)
        {
            pos.x = boundBox;
        }else if (pos.x < -boundBox)
        {
            pos.x = -boundBox;
        }else if (pos.z > boundBox)
        {
            pos.z = boundBox;
        }else if (pos.z < -boundBox)
        {
            pos.z = -boundBox;
        }

        transform.position = pos;
    }

    private void Pause()
    {
        print("called");
        // pausing the game
        if (isGamePaused == true && gM.isGameOver == false)
        {
            pauseMenu.SetActive(true);
            crosshairs.SetActive(false);
            Time.timeScale = 0;
            gM.isGameActive = false;
            print("pause");
        }

        // unpausing the game
        if (isGamePaused == false)
        {
            if(gM.isGameOver == false)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                crosshairs.SetActive(true);
                gM.isGameActive = true;
                print("unpause");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player lands on the ground it sets isOnGround to true
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // triggers the game over
        if (other.CompareTag("Cop"))
        {
            gM.isGameOver = true;
            gM.GameOver();
        }

        // adds to the flag count and score
        if (other.CompareTag("Flag"))
        {
            sM.spawnFlag = true;
            sM.spawnEnabled = true;
            gM.UpdateFlagCount(1);
            Destroy(other.gameObject);
            flagCount += 1;
        }
    }

    // idk how this works, it just does. this is for spawning the cop every 5 flags
    private int _flagCount;
    public int flagCount
    {
        get => _flagCount;
        private set
        {
            _flagCount = value;
            if(_flagCount % difficulty == 0)
            {
                sM.spawnEnabled = true;
                sM.spawnCop = true;
            }
        }
    }

    private void OnDisable()
    {
        // Again so the input system does not yell at me
        move.Disable();
        jump.Disable();
        pause.Disable();
        pov.Disable();
    }
}
