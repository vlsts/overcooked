using UnityEngine;

public class SupplyCounterVisual : MonoBehaviour
{
    [SerializeField] private SupplyCounter supplyParentCounter;

    private const string INTERACT = "Interact";
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        supplyParentCounter.OnSupplyCounterOpened += SupplyParentCounter_OnSupplyCounterOpened;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SupplyParentCounter_OnSupplyCounterOpened()
    {
        animator.SetTrigger(INTERACT);
    }
}
