using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseRaycast : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��������� ������� ����� ������ ����
        {
            CheckUIImageHit();
        }
    }

    void CheckUIImageHit()
    {
        // ������� ������� �� ������� ����
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // ������ ��� �������� ����������� ��������
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = FindObjectOfType<GraphicRaycaster>();

        // ��������� �������
        raycaster.Raycast(pointerData, results);

        // ���������, ����� �� ������� �� �����������
        foreach (RaycastResult result in results)
        {
            // ���������, ����� �� ������ ��������� Image
            Image image = result.gameObject.GetComponent<Image>();
            if (image != null)
            {
                Debug.Log("������� ����� �� �����������: " + result.gameObject.name);
                return; // ������� �� ������, ���� ������ �� �����������
            }
        }

        Debug.Log("������� �� ����� �� �����������.");
    }
}
