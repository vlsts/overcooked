using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float currentSpeed;
    private const float playerRadius = .7f;
    private const float playerHeight = 2f;

    public static Player Instance { get; private set; }

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
        HandlePlayerMovement();
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

        if (moveDirection.magnitude > 0)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }

        Debug.Log(moveDirection.magnitude);

        currentSpeed = moveDirection.magnitude;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
