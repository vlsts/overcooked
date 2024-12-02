using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Player : MonoBehaviour
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask countersLayerMask;

    public static Player Instance { get; private set; }
    
    private float currentSpeed;
    private const float playerRadius = .7f;
    private const float playerHeight = 2f;
    private BaseCounter selectedCounter;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(Instance);
    }

    void Start()
    {

    }

    void Update()
    {
        HandleInteractions();
        HandlePlayerMovement();
    }

    private void HandleInteractions()
    {
        Vector3 raycastDirection = transform.forward;
        float raycastDistance = 2f;

        if (Physics.Raycast(transform.position + Vector3.up * (playerHeight/2), raycastDirection, out RaycastHit raycastHit, raycastDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                selectedCounter = selectedCounter != baseCounter ? baseCounter : null;
            }
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
}
