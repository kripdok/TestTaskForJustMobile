using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField] Collider2D _collider;
    public float rayLength = 5f; // ƒлина луча
    public Color rayColor = Color.red; // ÷вет луча

    void Update()
    {
  
        Vector2 origin = (Vector2)_collider.bounds.min - new Vector2(0, 0.2f); 
        Vector2 origin2 = (Vector2)_collider.bounds.min - new Vector2(0, 0.2f);
        origin2.x += _collider.bounds.size.x;
  
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength);

        if (hit.collider != null)
        {
            Debug.DrawLine(origin, hit.point, rayColor);
            Debug.DrawLine(origin2, hit.point, rayColor);
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * rayLength, rayColor);
            Debug.DrawLine(origin2, origin2 + direction * rayLength, rayColor);
        }
    }

}
