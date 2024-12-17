using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private List<RecipeSO> availabileRecipesSO;

    public static DeliveryManager Instance { get; private set; }
    public event Action OnOrderAdded;
    public event EventHandler<OnOrderServedEventArgs> OnOrderServed;
    public class OnOrderServedEventArgs : EventArgs {
        public int servedOrderIndex;
    };

    private List<RecipeSO> orderedRecipesSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        orderedRecipesSO = new List<RecipeSO>();
    }

    private void Start()
    {
        StartCoroutine(SpawnRecipeCoroutine());
    }

    private IEnumerator SpawnRecipeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            SpawnNewRecipe();
        }
    }

    private void SpawnNewRecipe()
    {
        if (availabileRecipesSO.Count > 0 && orderedRecipesSO.Count < 5)
        {
            int randomIndex = UnityEngine.Random.Range(0, availabileRecipesSO.Count);

            orderedRecipesSO.Add(availabileRecipesSO[randomIndex]);

            OnOrderAdded?.Invoke();
        }
    }

    public bool DeliverPlate(List<KitchenObject> foodItemsOnPlate)
    {
        for (int i = 0; i < orderedRecipesSO.Count; i++)
        {
            if (IsRecipeMatched(orderedRecipesSO[i], foodItemsOnPlate))
            {
                orderedRecipesSO.RemoveAt(i);
                OnOrderServed?.Invoke(this, new OnOrderServedEventArgs { servedOrderIndex = i });
                return true;
            }
        }
        return false;
    }

    private bool IsRecipeMatched(RecipeSO recipe, List<KitchenObject> foodItemsOnPlate)
    {
        foreach (KitchenObjectSO recipeItem in recipe.kitchenObjectSOs)
        {
            bool isMatched = false;
            foreach (KitchenObject foodItem in foodItemsOnPlate)
            {
                if (foodItem.GetKitchenObjectSO().name == recipeItem.name)
                {
                    isMatched = true;
                    break;
                }
            }
            if (!isMatched)
            {
                return false;
            }
        }
        return true;
    }

    public List<RecipeSO> GetAllActiveOrders()
    {
        return orderedRecipesSO;
    }
}
