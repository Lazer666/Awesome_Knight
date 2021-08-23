using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D txt_cursor;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hot_spot = Vector2.zero;

    public GameObject mouse_pt;
    private GameObject ins_mouse;

    // Update is called once per frame
    void Update()
    {
        //https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
        Cursor.SetCursor(txt_cursor, hot_spot, mode);

        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    temp.y = 0.25f;

                    //Instantiate(mouse_pt, temp, Quaternion.identity);
                    if (ins_mouse == null)
                    {
                        ins_mouse = Instantiate(mouse_pt) as GameObject;
                        ins_mouse.transform.position = temp;
                    }
                    else
                    {
                        Destroy(ins_mouse);
                        ins_mouse = Instantiate(mouse_pt) as GameObject;
                        ins_mouse.transform.position = temp;
                    }
                }
            }
        }
    }
}
