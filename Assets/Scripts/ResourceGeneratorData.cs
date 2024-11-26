using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceGeneratorData
{
    public ResourceTypeSO resourceType;
    public float generationCooldown;
    public float resourceDetectionRadius;
    public int maxResourceAmounts;
}
