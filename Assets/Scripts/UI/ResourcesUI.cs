using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, Transform> resourceItems;
    ResourceTypeListSO resourceTypeList;
    private void Awake()
    {
        resourceItems = new Dictionary<ResourceTypeSO, Transform>();
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        Transform resourceItemTemplate = transform.Find("ResourceItemTemplate");
        resourceItemTemplate.gameObject.SetActive(false);

        foreach(ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceItem = Instantiate(resourceItemTemplate, transform);
            resourceItem.Find("image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceItem.gameObject.SetActive(true);
            resourceItems[resourceType] = resourceItem;
        }
    }

    private void Start()
    {
        UpdateResourceAmounts();
        ResourceManager.Instance.OnResourceAmountChanged += UpdateResourceAmounts;
    }

    private void UpdateResourceAmounts()
    {
        foreach(ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceItem = resourceItems[resourceType];
            resourceItem.Find("text").GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.GetResourceAmount(resourceType).ToString();
        }
    }
}
