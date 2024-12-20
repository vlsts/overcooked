using System;
using System.Collections;
using UnityEngine;

public class PlatesSpawningCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    public event Action OnPlateSpawned;
    public event Action OnPlateTaken;

    private int platesSpawned = 0;
    private int maxPlatesNumber = 3;

    void Start()
    {
        SpawnPlates();
    }

    private void SpawnPlates()
    {
        for (int i = 0; i < maxPlatesNumber; i++)
        {
            platesSpawned++;
            OnPlateSpawned?.Invoke();
        }
    }

    public override void Interact(Player player)
    {
        if (Player.Instance.HasKitchenObject())
        {
            //No plates at the moment
        }
        else
        {
            if (platesSpawned > 0)
            {
                if (KitchenObject.TrySpawnKitchenObject(plateKitchenObjectSO, Player.Instance))
                {
                    platesSpawned--;
                    OnPlateTaken?.Invoke();
                }
            }
        }
    }
}
