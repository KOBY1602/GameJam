using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Clamp the mouse position to stay within the screen boundaries
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, Screen.height);

        // Convert the clamped mouse position back to world coordinates
        Vector3 clampedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the cursor position to the clamped position
        Cursor.SetCursor(null, clampedMousePosition, CursorMode.Auto);
    }
}
