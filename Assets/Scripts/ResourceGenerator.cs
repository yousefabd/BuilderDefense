using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private float currentTimer;
    private float timerMax;
    private void Start()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().GetBuildingTypeSO().resourceGenerationData;
        timerMax = resourceGeneratorData.generationCooldown;
        currentTimer = timerMax;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceNodes = 0;
        foreach(Collider2D collider2D in collider2DArray)
        {
            if(collider2D.TryGetComponent(out  ResourceNode resourceNode))
            {
                if(resourceNode.Matches(resourceGeneratorData.resourceType))
                    nearbyResourceNodes++;
            }
        }
        nearbyResourceNodes = Mathf.Clamp(nearbyResourceNodes, 0, resourceGeneratorData.maxResourceAmounts);
        if(nearbyResourceNodes == 0)
            enabled = false;
        else
        {
            timerMax = (resourceGeneratorData.generationCooldown / 2f) +
                        resourceGeneratorData.generationCooldown *
                        (1 - (float)nearbyResourceNodes / resourceGeneratorData.maxResourceAmounts);
        }
        Debug.Log("When Nearby resources are " + nearbyResourceNodes + " Timer is: " + timerMax);
    }

    // Update is called once per frame
    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <0)
        {
            currentTimer = timerMax;
            ResourceManager.Instance.AddResourceAmount(resourceGeneratorData.resourceType, 1);
        }
    }
}
