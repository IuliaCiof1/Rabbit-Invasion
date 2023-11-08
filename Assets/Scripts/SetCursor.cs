using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] Texture2D texture;

    void Start()
    {
        Vector2 cursorHotspot = new Vector2(16,16); //texture size is 32x32 so the center would be at 16x16
        Cursor.SetCursor(texture, cursorHotspot ,CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
