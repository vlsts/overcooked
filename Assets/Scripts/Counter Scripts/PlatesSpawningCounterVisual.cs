using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatesSpawningCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesSpawningCounter supplyParentCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platePrefab;

    private List<GameObject> platesList;
    private const float plateOffsetY = 0.1f;

    private void Awake()
    {
        platesList = new List<GameObject>();
    }

    private void Start()
    {
        supplyParentCounter.OnPlateSpawned += SupplyParentCounter_OnPlateSpawned;
        supplyParentCounter.OnPlateTaken += SupplyParentCounter_OnPlateTaken;
    }

    private void SupplyParentCounter_OnPlateTaken()
    {
        Debug.Assert(platesList.Count > 0, "Attempting to take a plate when no plates are present in the list.");

        GameObject plateGameObject = platesList[platesList.Count - 1];
        platesList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void SupplyParentCounter_OnPlateSpawned()
    {
        Transform plateVisualTransform = Instantiate(platePrefab, counterTopPoint);
        plateVisualTransform.localPosition = GetNextPlatePosition();
        platesList.Add(plateVisualTransform.gameObject);
    }


    private Vector3 GetNextPlatePosition()
    {
        return new Vector3(0, plateOffsetY * platesList.Count, 0);
    }
}
