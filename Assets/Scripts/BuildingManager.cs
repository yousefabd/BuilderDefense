using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private Camera mainCamera;

    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO selectedBuilding;

    public event Action<BuildingTypeSO> OnSetBuilding;
    public event Action OnClearBuilding;
    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        selectedBuilding = null;
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }


    private void PlaceBuilding()
    {
        if (HasSelected() && !EventSystem.current.IsPointerOverGameObject() && CanPlaceBuilding())
            Instantiate(selectedBuilding.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
    }
    private bool CanPlaceBuilding()
    {
        BoxCollider2D boxCollider = selectedBuilding.prefab.GetComponent<BoxCollider2D>();
        //check if overlaps any buildings
        Collider2D[] collidedBuildings = Physics2D.OverlapBoxAll(UtilsClass.GetMouseWorldPosition(), boxCollider.size, 0);
        if (collidedBuildings.Length > 0) 
        {
            Debug.Log("Overlaps another building");
            return false;
        }
        //--------------------------------
        //check if nearby buildings of the same type are further than the allowed min radius
        Collider2D[] nearbyTypes = Physics2D.OverlapCircleAll(UtilsClass.GetMouseWorldPosition(), selectedBuilding.minConstructionRadius);
        foreach(Collider2D nearbyBuilding in nearbyTypes)
        {
            BuildingTypeSO nearbyBuildingTypeSO = nearbyBuilding.GetComponent<BuildingTypeHolder>()?.GetBuildingTypeSO();
            if (nearbyBuildingTypeSO == selectedBuilding)
            {
                Debug.Log("Too close to buildings of the same type");
                return false;
            }
        }
        //--------------------------------
        //check if there are nearby buildings (any) below the max construction distance
        Collider2D[] nearbyBuildings = Physics2D.OverlapCircleAll(UtilsClass.GetMouseWorldPosition(), selectedBuilding.maxConstructionRadius);
        foreach(Collider2D nearbyCollider in nearbyBuildings)
        {
            if (nearbyCollider.TryGetComponent<BuildingTypeHolder>(out _))
            {
                return true;
            }
        }
        return false;
    }
    public void SetSelectedBuilding(BuildingTypeSO buildingType)
    {
        selectedBuilding = buildingType;
        OnSetBuilding?.Invoke(buildingType);
    }

    public void ClearSelectedBuilding()
    {
        selectedBuilding = null;
        OnClearBuilding?.Invoke();
    }

    public bool IsSelected(BuildingTypeSO buildingType)
    {
        return selectedBuilding == buildingType;
    }

    public bool HasSelected()
    {
        return selectedBuilding != null;
    }

}
