using System;
using System.Collections;
using UnityEngine;

public class PlatesSpawningCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    //public event EventHandler OnPlateSpawned;
    //public event EventHandler OnPlateTaken;

    private const float spawnWaitTime = 4f;
    private int platesSpawned = 0;
    private int maxPlatesNumber = 3;
    private Coroutine spawnPlateCoroutine;

    void Start()
    {
        spawnPlateCoroutine = StartCoroutine(SpawnPlateCoroutine());
    }

    private IEnumerator SpawnPlateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnWaitTime);

            if (platesSpawned < maxPlatesNumber)
            {
                platesSpawned++;
                Debug.Log($"Spawned plate {platesSpawned}");
                //OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (Player.Instance.HasKitchenObject())
        {
            //No plates at the moment
            Debug.Log("Player has object?");
        }
        else
        {
            Debug.Log("Player has no kitchen object");
            if (platesSpawned > 0)
            {
                Debug.Log("Give plate");
                KitchenObject.TrySpawnKitchenObject(plateKitchenObjectSO, Player.Instance);
            }
        }
    }
}
