using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform holdPoint;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    public static Player Instance { get; private set; }
    
    private float currentSpeed;
    private const float playerRadius = .7f;
    private const float playerHeight = 2f;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


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
    }

    void Start()
    {
        GameInput.Instance.OnPrimaryInteractAction += GameInput_OnPrimaryInteractAction;
        GameInput.Instance.OnSecondaryInteractAction += GameInput_OnSecondaryInteractAction;
    }

    private void GameInput_OnPrimaryInteractAction()
    {
        if (!selectedCounter)
            return;
        if (selectedCounter is SinkCounter sinkCounter)
        {
            sinkCounter.OnTapToggle += SinkCounter_OnTapToggle;
        }    
        selectedCounter.Interact(this);
    }

    private void GameInput_OnSecondaryInteractAction()
    {
        if (!selectedCounter)
            return;
        selectedCounter.InteractSecondary(this);
    }

    private void SinkCounter_OnTapToggle(bool isWashing)
    {
        if (!isWashing)
            (selectedCounter as SinkCounter).OnTapToggle -= SinkCounter_OnTapToggle;
        GameInput.Instance.ToggleMovement(isWashing);
    }

    void Update()
    {
        HandleInteractions();
        HandlePlayerMovement();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        float raycastDistance = 2f;

        if (moveDirection != Vector3.zero)
            lastInteractDirection = moveDirection;

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, raycastDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                    selectedCounter = baseCounter;
            }
            else selectedCounter = null;
        }
        else
        {
            selectedCounter = null;
        }

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    void HandlePlayerMovement()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);

        if (!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance))
        {
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
        }
        else if (inputVector.x != 0 && inputVector.y != 0)
        {
            if (!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, new Vector3(moveDirection.x, 0, 0).normalized, moveDistance))
            {
                transform.position += moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0, 0).normalized;
            }
            else if (!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, new Vector3(0, 0, moveDirection.z).normalized, moveDistance))
            {
                transform.position += moveSpeed * Time.deltaTime * new Vector3(0, 0, moveDirection.z).normalized;
            }
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        currentSpeed = moveDirection.magnitude;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public bool SetKitchenObject(KitchenObject pickedKitchenObject)
    {
        if (!kitchenObject)
        {
            kitchenObject = pickedKitchenObject;
            return true;
        }
        return false;
    }
    
    public void RemoveKitchenObject()
    {
        kitchenObject = null;
    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
