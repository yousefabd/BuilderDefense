using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string stringName;
    public Transform prefab;
    public Sprite sprite;
    public ResourceGeneratorData resourceGenerationData;
    public float minConstructionRadius;
    public float maxConstructionRadius;
}
