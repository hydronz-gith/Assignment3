using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;
    InventoryHandler inventoryUI;
    InventoryDescription itemDescription;
    Rigidbody playerRigidbody;
    //CombatHandler combatHandler;
    PlayerHP playerHP;
    
    [Header("Camera")]
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    [Header("Inputs")]
    public bool b_Input;
    public bool openMenu_Input;
    public bool devCombat_Input;
    public bool inventory_Input;
    //public bool devDamage_Input;

    [Header("Menu")]
    public GameObject menuCanvas;
    public GameObject combatMenu;
    public GameObject inventoryMenu;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerRigidbody = GetComponent<Rigidbody>();
        menuCanvas.SetActive(false);
        combatMenu.SetActive(false);
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            
            playerControls.PlayerActions.B.performed += i => b_Input = true;
            playerControls.PlayerActions.B.canceled += i => b_Input = false;

            playerControls.PlayerActions.OpenMenu.performed += i => openMenu_Input = true;
            
            playerControls.PlayerActions.DevCombatMenu.performed += i => devCombat_Input = true;

            playerControls.PlayerActions.Inventory.performed += i => inventory_Input = true;

            //playerControls.PlayerActions.DevLoseDamage.performed += i => devDamage_Input = true;
        }

        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        OpenMenu();
        OpenCombatDevMenu();
        OpenInventory();
        //DevDamageInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
        
        // Abs is short for Absolute; meaning it removes the negative value of a value if any
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (b_Input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    //public void DevDamageInput()
    //{
    //    if (devDamage_Input)
    //    {
    //        devDamage_Input = false;
    //        playerHP.Reduce(damage:10);
    //        //combatHandler.loseDamage = 1;
    //    }
    //}

    public void OpenMenu()
    {
        if (openMenu_Input)
        {
            openMenu_Input = false;
            playerControls.PlayerMovement.Disable();
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }

            if (menuCanvas.activeSelf == false)
            {
                playerControls.PlayerMovement.Enable();
                openMenu_Input = false;
                
            }
        }

    public void OpenCombatDevMenu()
    {
        if (devCombat_Input)
        {
            devCombat_Input = false;
            playerControls.PlayerMovement.Disable();
            combatMenu.SetActive(!combatMenu.activeSelf);
        }

            if (combatMenu.activeSelf == false)
            {
                playerControls.PlayerMovement.Enable();
                devCombat_Input = false;
                
            }
        }

    public void OpenInventory()
    {
        if (inventory_Input)
        {
            inventory_Input = false;
            //Great example of calling a method from another class.
            // If we want InventoryHandler, we put that and how we want to refer to it here.
            // In this case its inventoryUI; now we call a method that's WITHIN that class.
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            //inventoryUI.Show();
        }

        if (inventoryMenu.activeSelf == false)
        {
            inventory_Input = false;
        }
    }
}

