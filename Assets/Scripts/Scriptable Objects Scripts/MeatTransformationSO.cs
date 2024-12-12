using UnityEngine;

[CreateAssetMenu(fileName = "MeatTransformationSO", menuName = "Scriptable Objects/MeatTransformationSO")]
public class MeatTransformationSO : ScriptableObject
{
    public KitchenObjectSO rawMeat;
    public KitchenObjectSO cookedMeat;
    public KitchenObjectSO burnedMeat;
}
