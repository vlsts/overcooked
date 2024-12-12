using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.RemoveKitchenObject();
        }
        
        kitchenObjectParent = newKitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetHoldPoint();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public static bool TrySpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent newKitchenObjectParent)
    {
        if (newKitchenObjectParent.HasKitchenObject())
            return false;
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(newKitchenObjectParent);

        return true;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.RemoveKitchenObject();
        Destroy(gameObject);
    }
}
