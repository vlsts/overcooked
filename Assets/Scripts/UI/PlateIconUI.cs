using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetImageSprite(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
        image.GetComponent<Image>().preserveAspect = true;
    }
}
