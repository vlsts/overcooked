using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter parentCuttingCounter;
    
    private Animator animator;
    private const string CUT = "Cut";

    void Start()
    {
        animator = GetComponent<Animator>();
        parentCuttingCounter.OnObjectCut += ParentCuttingCounter_OnObjectCut;
    }

    private void ParentCuttingCounter_OnObjectCut()
    {
        animator.SetTrigger(CUT);
    }
}
