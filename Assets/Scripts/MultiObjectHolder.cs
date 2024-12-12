using System.Collections.Generic;
using UnityEngine;


public class MultiObjectHolder : MonoBehaviour
{
    private List<KitchenObject> kitchenObjects;
    private Bread breadOnPlate;
    private Vector3 startingPositionFoodInsideTheBread;

    private void Awake()
    {
        startingPositionFoodInsideTheBread = new Vector3(0, 0.05f, 0);
        kitchenObjects = new List<KitchenObject>();
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        if (kitchenObjects.Contains(kitchenObject))
            return;
        if (kitchenObject is Bread bread)
        {
            breadOnPlate = bread;
            bread.transform.position = transform.position;

            kitchenObjects.Insert(0, kitchenObject);

            foreach (var obj in kitchenObjects)
            {
                if (obj != bread)
                    breadOnPlate.LiftTopBreadSlice(obj.GetKitchenObjectSO().heightOffset / 2);
            }
        }
        else
        {
            if (breadOnPlate)
            {
                breadOnPlate.LiftTopBreadSlice(kitchenObject.GetKitchenObjectSO().heightOffset / 2);
            }
            kitchenObjects.Add(kitchenObject);
            PositionNewObject(kitchenObject);
        }
    }

    private void PositionNewObject(KitchenObject kitchenObject)
    {
        if ((kitchenObjects.Count == 2 && breadOnPlate) || kitchenObjects.Count == 1)
        {
            kitchenObject.transform.position += startingPositionFoodInsideTheBread;
        }
        else
        {
            kitchenObject.transform.position = new Vector3(kitchenObject.transform.position.x, CalculateStackHeightFor(kitchenObject), kitchenObject.transform.position.z);
        }
    }

    private float CalculateStackHeightFor(KitchenObject kitchenObject)
    {
        return kitchenObjects[kitchenObjects.Count - 2].transform.position.y + kitchenObject.GetKitchenObjectSO().heightOffset;
    }

    public void RemoveKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObjects.Remove(kitchenObject);
    }

    public void RemoveAllKitchenObjects()
    {
        kitchenObjects.Clear();
    }

    public List<KitchenObject> GetAllKitchenObjects()
    {
        return kitchenObjects;
    }

    public bool HasKitchenObjects()
    {
        return kitchenObjects.Count > 0;
    }
}

