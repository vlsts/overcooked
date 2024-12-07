using UnityEngine;

[CreateAssetMenu(fileName = "CuttableFoodSO", menuName = "Scriptable Objects/CuttableFoodSO")]
public class CuttableFoodSO : ScriptableObject
{
    public KitchenObjectSO originalFood;
    public KitchenObjectSO cutFood;
    public int necessaryCuts;
}
