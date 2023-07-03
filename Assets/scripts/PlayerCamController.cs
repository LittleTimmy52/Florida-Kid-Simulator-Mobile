using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamController : MonoBehaviour
{
    #region PUBLIC FEILDS

    [Header("Camera Rotate X Settings")] public float cameraRotateXMin;
    public float cameraRotateXMax;
    [Header("Mouse Smoothing")] public float mouseSmooth;
    [Header("Y Cam Rotate")] public float yCamRotate;

    #endregion

    #region PRIVATE FILDS
        
    private float m_mouseX;
    private float m_mouseY;
    private float m_rotateX;
    private Transform m_parent;
    private Camera m_camera;
    private GameManager gM;

    private GetInp inp;
    private InputAction look;

    #endregion

    #region UNITY ROUTINES

    private void Awake()
    {
        #region INITALIZE FEILDS

        m_camera = gameObject.GetComponent<Camera>();
        m_parent = transform.parent;

        gM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        #endregion

        inp = new GetInp();
    }

    private void OnEnable()
    {
        look = inp.Player.Look;

        look.Enable();
    }

    private void Update()
    {
        mouseSmooth = PlayerPrefs.GetFloat("Sensitivity");

        MouseInputs();

        if (gM.isGameActive == true)
        {
            RotatePlayerY();
            RotateCameraX();
        }
    }

    #endregion

    #region HELPER ROUTINES

    private void MouseInputs()
    {
        #region  COLLECT MOUSE INPUTS

        m_mouseX = look.ReadValue<Vector2>().x;
        m_mouseY = look.ReadValue<Vector2>().y;

        #endregion
    }

    private void RotatePlayerY()
    {
        #region  ROTATE PLAYER Y

        m_parent.Rotate(Vector3.up * m_mouseX);

        #endregion
    }

    private void RotateCameraX()
    {
        #region  ROTATE CAMERA X AXIS
        
        m_rotateX += m_mouseY;
        m_rotateX = Mathf.Clamp(m_rotateX, cameraRotateXMin, cameraRotateXMax);
        m_camera.transform.localRotation = Quaternion.Euler(-m_rotateX, yCamRotate, 0f);

        #endregion
    }

    private void OnDisable()
    {
        look.Disable();
    }

    #endregion
}
