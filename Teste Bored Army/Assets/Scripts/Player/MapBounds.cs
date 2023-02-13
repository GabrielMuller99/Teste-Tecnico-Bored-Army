using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    void Update()
    {
        float limitX = Mathf.Clamp(transform.position.x,
            Camera.main.ViewportToWorldPoint(Vector3.zero).x,
            Camera.main.ViewportToWorldPoint(Vector3.one).x);

        float limitY = Mathf.Clamp(transform.position.y,
            Camera.main.ViewportToWorldPoint(Vector3.zero).y,
            Camera.main.ViewportToWorldPoint(Vector3.one).y);

        transform.position = new Vector2(limitX, limitY);
    }
}
