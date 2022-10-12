using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;
    private Transform zombie;
    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public GameObject life1, life2, life3,life4,life5;
    private int life = 5;
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void Start()
    {
        zombie = GameObject.FindWithTag("Zombie").transform;
    }
    void gethit()
    {
        float distance = Vector3.Distance(zombie.position, transform.position);
        if(distance < 2)
        {
            life--;
            Life();
        }
    }
    void Life()
    {
        if (life == 3)
        {
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        if (life == 2)
        {
            life3.SetActive(false);
            Object.Destroy(life3);
            life1.SetActive(true);
        }
        if (life == 1)
        {
            life3.SetActive(false);
            Object.Destroy(life2);
            life1.SetActive(true);
        }
        if (life < 1)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            Object.Destroy(life1);
        }
    }
}
