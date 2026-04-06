using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero; 
    public CursorMode cursorMode = CursorMode.Auto;

    public void ChangeCursor()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
