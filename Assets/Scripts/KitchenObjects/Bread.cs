using UnityEngine;

public class Bread : KitchenObject
{
    [SerializeField] private Transform topBreadSlice;
    [SerializeField] private Transform bottomBreadSlice;

    public void LiftTopBreadSlice(float height)
    {
        Vector3 currentPosition = topBreadSlice.localPosition;
        topBreadSlice.localPosition = new Vector3(currentPosition.x, currentPosition.y + height, currentPosition.z);
    }
}
