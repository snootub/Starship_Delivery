using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool is2DSprite = true;
    // Inspiration from https://www.youtube.com/watch?v=KGG2V4ZkXTg
    private void LateUpdate()
    {
        // Gettting the camera position
        Vector3 cameraPosition = Camera.main.transform.position;
        
        // Only rotate on the Y axis:
        cameraPosition.y = transform.position.y;

        // Make the object face the camera
        transform.LookAt(cameraPosition);

        if (is2DSprite)
        {
            // Rotate 180 on Y because of SpriteRenderer points away from camera
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
