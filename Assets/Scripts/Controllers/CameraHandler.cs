using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    float orthographicSize;
    float targetOrthographicSize;

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleLens();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float moveSpeed = 22f;
        Vector3 moveDir = new Vector3(x, y).normalized;

        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    private void HandleLens()
    {
        float zoomValue = 2f;
        float zoomSpeed = 5f;

        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomValue;
        float minOrthographicSize = 10f;
        float maxOrthographicSize = 20f;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, zoomSpeed*Time.deltaTime);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
