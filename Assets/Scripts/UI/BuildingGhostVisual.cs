using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhostVisual : MonoBehaviour
{
    private void Start()
    {
        BuildingManager.Instance.OnSetBuilding += BuildingManager_OnSetBuilding;
        BuildingManager.Instance.OnClearBuilding += BuildingManager_OnClearBuilding;
        gameObject.SetActive(false);
    }

    private void BuildingManager_OnClearBuilding()
    {
        gameObject.SetActive(false);
    }

    private void BuildingManager_OnSetBuilding(BuildingTypeSO buildingType)
    {
        GetComponent<SpriteRenderer>().sprite = buildingType.sprite;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }
}
