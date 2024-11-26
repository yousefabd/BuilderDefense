using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectorUI : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite;
    private Transform cursorTransform;

    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buildingTypeButtons;

    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingTypeButtons = new Dictionary<BuildingTypeSO, Transform>();

        Transform buttonTemplate = transform.Find("BuildingTypeButtonTemplate");
        buttonTemplate.gameObject.SetActive(false);
        Transform cursorButton = Instantiate(buttonTemplate, transform);
        cursorButton.Find("sprite").GetComponent<Image>().sprite = cursorSprite;
        cursorButton.gameObject.SetActive(true);
        cursorButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.ClearSelectedBuilding();
            UpdateButtons();
        });
        cursorButton.Find("selected").gameObject.SetActive(true);
        cursorTransform = cursorButton;
        foreach (BuildingTypeSO buildingTypeSO in buildingTypeList.list)
        {
            Transform buildingTypeButton = Instantiate(buttonTemplate, transform);
            buildingTypeButton.Find("sprite").GetComponent<Image>().sprite = buildingTypeSO.sprite;
            buildingTypeButton.gameObject.SetActive(true);
            buildingTypeButton.Find("selected").gameObject.SetActive(false);
            buildingTypeButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetSelectedBuilding(buildingTypeSO);
                UpdateButtons();
            });
            buildingTypeButtons[buildingTypeSO] = buildingTypeButton;
        }
    }

    private void UpdateButtons()
    {
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            buildingTypeButtons[buildingType].Find("selected").gameObject.SetActive(BuildingManager.Instance.IsSelected(buildingType));
        }
        cursorTransform.Find("selected").gameObject.SetActive(!BuildingManager.Instance.HasSelected());
    }
}
