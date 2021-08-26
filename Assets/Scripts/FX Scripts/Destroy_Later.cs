using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Later : MonoBehaviour
{
    public float timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

}
