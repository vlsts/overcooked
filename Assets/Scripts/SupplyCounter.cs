using UnityEngine;

public class SupplyCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(Player player)
    {
        Debug.Log("SupplyCounter.Interact()");
    }
}
