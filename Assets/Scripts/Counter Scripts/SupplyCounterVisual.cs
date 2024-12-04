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

    private void SupplyParentCounter_OnSupplyCounterOpened()
    {
        animator.SetTrigger(INTERACT);
    }
}
