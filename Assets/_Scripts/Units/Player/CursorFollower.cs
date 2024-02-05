using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    [SerializeField]
    private bool canFollow = true;

    void Update()
    {
        if (!canFollow) { return; }

        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Make sure the z-coordinate is 0

        // Calculate the direction from the sprite to the mouse position
        Vector3 lookDir = mousePos - transform.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // Rotate the sprite to face the cursor
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    public void EnableFollow()
    {
        canFollow = true;
    }

    public void DisableFollow()
    {
        canFollow = false;
    }
}
