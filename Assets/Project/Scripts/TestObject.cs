using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public float rayLength = 5f; // ����� ����
    public Color rayColor = Color.red; // ���� ����

    void Update()
    {
        // �������� ��������� �������
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogWarning("Collider2D �� ������ �� �������!");
            return;
        }

        // ���������� ������ ������� �������
        Vector2 origin = (Vector2)collider.bounds.min - new Vector2(0,0.2f); // ������ ����� ����� ����������
        origin.x += collider.bounds.size.x / 2; // ���������� �� ��� X

        // ���������� ����������� (����)
        Vector2 direction = Vector2.down;

        // ��������� ������� � �������������� layerMask
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength);

        // ���� ��� ����� � ������, ���������� ����� �� ����� ���������
        if (hit.collider != null)
        {
            Debug.Log("���");
            Debug.DrawLine(origin, origin + direction * rayLength, rayColor);
        }
        else
        {
            Debug.Log("�� ���");
            // ���� �� �����, ���������� ����� �� ������������ �����
            Debug.DrawLine(origin, origin + direction * rayLength, Color.green);
        }
    }
}
