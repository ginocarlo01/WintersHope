using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMirror : MonoBehaviour
{
    [SerializeField]
    string projectileTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(projectileTag))
        {
            BounceObject(collision.gameObject);
        }
    }

    private void BounceObject(GameObject obj)
    {
        Quaternion currentRotation = obj.transform.rotation;

        // Reflect the rotation by flipping the sign of the Z component
        currentRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, -currentRotation.eulerAngles.z);

        // Apply the new rotation
        obj.transform.rotation = currentRotation;
    }
}
