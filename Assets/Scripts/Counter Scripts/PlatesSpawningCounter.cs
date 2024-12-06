using System;
using System.Collections;
using UnityEngine;

public class PlatesSpawningCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    public event Action OnPlateSpawned;
    public event Action OnPlateTaken;

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
                OnPlateSpawned?.Invoke();
            }
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
