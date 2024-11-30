using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float currentSpeed;

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
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);

        transform.position += moveSpeed * Time.deltaTime * moveDirection;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        currentSpeed = moveDirection.magnitude;
    }

    public float GetCurrentSpeed()
    {
        Debug.Log(currentSpeed);
        return currentSpeed;
    }
}
