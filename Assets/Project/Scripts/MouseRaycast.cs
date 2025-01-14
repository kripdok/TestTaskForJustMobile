using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseRaycast : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажатие левой кнопки мыши
        {
            CheckUIImageHit();
        }
    }

    void CheckUIImageHit()
    {
        // Создаем рейкаст от позиции мыши
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Список для хранения результатов рейкаста
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = FindObjectOfType<GraphicRaycaster>();

        // Выполняем рейкаст
        raycaster.Raycast(pointerData, results);

        // Проверяем, попал ли рейкаст на изображение
        foreach (RaycastResult result in results)
        {
            // Проверяем, имеет ли объект компонент Image
            Image image = result.gameObject.GetComponent<Image>();
            if (image != null)
            {
                Debug.Log("Рейкаст попал на изображение: " + result.gameObject.name);
                return; // Выходим из метода, если попали на изображение
            }
        }

        Debug.Log("Рейкаст не попал на изображение.");
    }
}
