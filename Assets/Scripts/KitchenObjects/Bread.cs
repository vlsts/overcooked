using UnityEngine;

public class Bread : KitchenObject
{
    [SerializeField] private Transform topBreadSlice;
    [SerializeField] private Transform bottomBreadSlice;

    private const float ERROR_OFFSET = 0.05f;

    public void LiftTopBreadSlice(float height)
    {
        Vector3 currentPosition = topBreadSlice.localPosition;
        topBreadSlice.localPosition = new Vector3(currentPosition.x, currentPosition.y + height + ERROR_OFFSET, currentPosition.z);
    }
}
