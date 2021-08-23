using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D txt_cursor;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hot_spot = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
        Cursor.SetCursor(txt_cursor, hot_spot, mode);
    }
}
