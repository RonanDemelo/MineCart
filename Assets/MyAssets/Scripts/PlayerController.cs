using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public PlayerInput playerInput;
    [SerializeField]
    private CinemachineCamera cam;

    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioSource gemPickUp;

    [Header("Movement")]
    [SerializeField, Range(0f, 100f)]
    public float maxSpeed = 0f;


    [Header("Camera")]
    [SerializeField]
    private float currentPitch = 0f;
    private float currentYaw = 0f;
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

    public float CurrentYaw
    {
        get => currentYaw;
        set
        {
            currentYaw = Mathf.Clamp(value, -pitchRange, pitchRange);
        }
    }

    [Header("Light")]
    [SerializeField]
    GameObject flashLight;
    [SerializeField]
    float currentBattery, maxBattery;
    [SerializeField]
    float lightCost;
    [SerializeField]
    Slider battery;
    [SerializeField]
    float rayLength;
    [SerializeField]
    LayerMask layerMask;

    [Header("PickUp")]
    [SerializeField]
    float pickUpDis = 10f;
    [SerializeField]
    LayerMask pickUpLayerMask;
    [SerializeField]
    float cooldownTime = 1f;
    [SerializeField]
    float cooldownLeft = 1f;
    bool cooldownComplete = true;

    [Header("Score")]
    [SerializeField]
    public float score = 0f;
    [SerializeField]
    TextMeshProUGUI scoreText;

    //input action
    private InputAction pickUpAction;
    private InputAction lookAction;
    private InputAction lightAction;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gemPickUp = GetComponent<AudioSource>();

        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        lookAction = playerInput.actions["Look"];
        lightAction = playerInput.actions["Light"];
        pickUpAction = playerInput.actions["PickUp"];
    }

    private void Update()
    {
        Look();
        Move();
        FlashOn();
        PickUp();
        battery.value = currentBattery / maxBattery;
        StartCooldown();
        scoreText.text = score.ToString("F0");

    }

    private void Move()
    {
        Vector3 _moveDir = transform.forward * 1f;
        _moveDir.y = 0;

        characterController.Move(_moveDir * maxSpeed * Time.deltaTime);
    }

    private void Look()
    {
        Vector2 _lookDir = lookAction.ReadValue<Vector2>() * mouseSens;

        //looking up and down
        CurrentPitch -= _lookDir.y;
        CurrentYaw += _lookDir.x;
        cam.transform.localRotation = Quaternion.Euler(CurrentPitch, CurrentYaw, 0f);
    }

    private void FlashOn()
    {
        if(lightAction.ReadValue<float>() < 0.5f)
        {
            flashLight.SetActive(false);
        }
        else if(lightAction.ReadValue<float>() > 0.5f && currentBattery > 0)
        {
            flashLight.SetActive(true);

            currentBattery -= lightCost * Time.deltaTime;
            if(currentBattery < 0f)
            {
                flashLight.SetActive(false);
                currentBattery = 0f;
            }

            //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit _rayHit, rayLength, layerMask))
            //{
            //    Debug.Log(_rayHit.collider);
            //    if (_rayHit.transform.TryGetComponent(out EnemyAI enemy))
            //    {
                    
            //    }
            //}

        }
    }

    private void PickUp()
    {
        if(pickUpAction.ReadValue<float>() > 0.5f)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit _rayHit, pickUpDis, pickUpLayerMask))
            {
                if (_rayHit.transform.TryGetComponent(out Gem gem))
                {
                    currentBattery += gem.chargeAmount;
                    score += gem.chargeAmount;

                    if (currentBattery > 100f)
                    {
                        currentBattery = 100f;
                    }
                    gemPickUp?.Play();
                    Destroy(gem.gameObject);
                }
            }


            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit _leverHit, pickUpDis + 3, pickUpLayerMask) && cooldownComplete == true)
            {
                if (_leverHit.transform.TryGetComponent(out SwitchTrack switchTrack))
                {
                    switchTrack.ChangeTrack();
                    cooldownLeft = 0f;
                    cooldownComplete = false;
                }
            }
        }
            
    }

    public void StartCooldown()
    {
        if(cooldownLeft <= cooldownTime)
        {
            cooldownLeft += 3f * Time.deltaTime;
        }
        if (cooldownLeft >= cooldownTime)
        {
            cooldownComplete = true;
        }
    }

}
