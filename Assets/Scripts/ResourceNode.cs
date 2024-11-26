using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceTypeSO resourceTypeSO;

    public bool Matches(ResourceTypeSO resourceTypeSO)
    {
        return resourceTypeSO == this.resourceTypeSO;
    }
}
