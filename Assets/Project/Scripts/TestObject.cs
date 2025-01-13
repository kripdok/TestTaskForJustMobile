using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public float rayLength = 5f; // Длина луча
    public Color rayColor = Color.red; // Цвет луча

    void Update()
    {
        // Получаем коллайдер объекта
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogWarning("Collider2D не найден на объекте!");
            return;
        }

        // Определяем нижнюю границу объекта
        Vector2 origin = (Vector2)collider.bounds.min - new Vector2(0,0.2f); // Нижняя левая точка коллайдера
        origin.x += collider.bounds.size.x / 2; // Центрируем по оси X

        // Определяем направление (вниз)
        Vector2 direction = Vector2.down;

        // Выполняем рэйкаст с использованием layerMask
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength);

        // Если луч попал в объект, отображаем линию до точки попадания
        if (hit.collider != null)
        {
            Debug.Log("Лох");
            Debug.DrawLine(origin, origin + direction * rayLength, rayColor);
        }
        else
        {
            Debug.Log("не лох");
            // Если не попал, отображаем линию на максимальную длину
            Debug.DrawLine(origin, origin + direction * rayLength, Color.green);
        }
    }
}
