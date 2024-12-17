using UnityEngine;

public class Bread : KitchenObject
{
    [SerializeField] private Transform topBreadSlice;
    [SerializeField] private Transform bottomBreadSlice;

    private float errorOfsset = 0.05f;

    public void LiftTopBreadSlice(float height)
    {
        Vector3 currentPosition = topBreadSlice.localPosition;
        topBreadSlice.localPosition = new Vector3(currentPosition.x, currentPosition.y + height + errorOfsset, currentPosition.z);
    }
}
