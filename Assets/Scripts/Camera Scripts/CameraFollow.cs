using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float fo_height = 8f;
    public float fo_distance = 6f;
    private Transform player;
    private float tar_height;
    private float cur_height;
    private float cur_rotation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        tar_height = player.position.y + fo_height;
        cur_rotation = transform.eulerAngles.y;
        cur_height = Mathf.Lerp(transform.position.y, tar_height, 0.9f * Time.deltaTime);
        Quaternion euler = Quaternion.Euler(0f, cur_rotation, 0f);
        Vector3 tar_position = player.position - (euler * Vector3.forward) * fo_distance;
        tar_position.y = cur_height;
        transform.position = tar_position;
        transform.LookAt(player);
    }
}
