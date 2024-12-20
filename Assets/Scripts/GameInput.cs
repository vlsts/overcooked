using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event Action OnPrimaryInteractAction;
    public event Action OnSecondaryInteractAction;

    private PlayerInputActions playerInputActions;

    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.PrimaryInteract.performed += PrimaryInteract_performed;
        playerInputActions.Player.SecondaryInteract.performed += SecondaryInteract_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.PrimaryInteract.performed -= PrimaryInteract_performed;
        playerInputActions.Player.SecondaryInteract.performed -= SecondaryInteract_performed;

        playerInputActions.Dispose();
    }

    private void SecondaryInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSecondaryInteractAction?.Invoke();
    }

    private void PrimaryInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPrimaryInteractAction?.Invoke();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public void ToggleMovement(bool isWashing)
    {
        if (isWashing) 
        {
            playerInputActions.Player.Move.Disable();
        }
        else
        {
            playerInputActions.Player.Move.Enable();
        }
    }
}
