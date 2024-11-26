using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> resourceAmounts;

    public event Action OnResourceAmountChanged;

    private void Awake()
    {
        Instance = this;

        resourceAmounts = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceTypeSO in resourceTypeList.list)
        {
            resourceAmounts[resourceTypeSO] = 0;
        }
    }

    private void TestLogResourceAmounts()
    {
        foreach(ResourceTypeSO resourceType in resourceAmounts.Keys)
        {
            Debug.Log(resourceType.stringName + ": " + resourceAmounts[resourceType]);
        }
        Debug.Log("-----------------------------------------------");
    }
    public void AddResourceAmount(ResourceTypeSO resourceType, int amount)
    {
        resourceAmounts[resourceType] += amount;
        OnResourceAmountChanged?.Invoke();
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmounts[resourceType];
    }
}
