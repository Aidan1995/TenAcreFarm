using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetZ = 5f;
    public float smoothing = -2f;

    // player transform component
    Transform playerPos;
    void Start()
    {        
        // Find the player gameobject in the scene and get its transform component
        playerPos = FindObjectOfType<PlayerController>().transform;
    }
    
    void Update()
    {
        FollowPlayer();
    }

    //Following the player
    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z - offsetZ);

        // Set the position accordingly
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
