using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypeHolder : MonoBehaviour
{
    [SerializeField] private BuildingTypeSO buildingTypeSO;

    public BuildingTypeSO GetBuildingTypeSO()
    {
        return buildingTypeSO;
    }
}
