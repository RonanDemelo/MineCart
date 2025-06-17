using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    [SerializeField]
    private CinemachineCamera cam;

    [Header("Movement")]
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 5f;


    [Header("Camera")]
    [SerializeField]
    private float currentPitch = 0f;
    private float mouseSens = 0.1f;
    private float pitchRange = 80f;
    public float CurrentPitch
    {
        get=> currentPitch;
        set
        {
            currentPitch = Mathf.Clamp(value, -pitchRange, pitchRange);
        }
    }

    //input action
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction lightAction;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        moveAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void Move()
    {
        Vector3 _moveDir = transform.forward * moveAction.ReadValue<Vector2>().y + transform.right * moveAction.ReadValue<Vector2>().x;
        _moveDir.y = 0;
        _moveDir.Normalize();

        characterController.Move(_moveDir * maxSpeed * Time.deltaTime);
    }

    private void Look()
    {
        Vector2 _lookDir = lookAction.ReadValue<Vector2>() * mouseSens;

        //looking up and down
        CurrentPitch -= _lookDir.y;
        cam.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        //looking left and right
        transform.Rotate(Vector3.up * _lookDir.x);
    }
}
