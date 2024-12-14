using UnityEngine;

public class PlateUI : MonoBehaviour
{
    [SerializeField] private Plate plateParent;
    [SerializeField] private Transform iconTemplate;

    private void Start()
    {
        plateParent.OnIngredientAdded += PlateParent_OnIngredientAdded;
        plateParent.OnClearPlate += PlateParent_OnClearPlate;
    }

    private void PlateParent_OnClearPlate()
    {
        ClearExistingIcons();
    }

    private void PlateParent_OnIngredientAdded()
    {
        ClearExistingIcons();

        foreach (KitchenObject kitchenObject in plateParent.GetAllKitchenObjects())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconUI>().SetImageSprite(kitchenObject.GetKitchenObjectSO());
        }
    }

    private void ClearExistingIcons()
    {
        foreach (Transform child in transform)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
