using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;

    public float horizontalFollowOffset;
    public float verticalFollowOffset;
    public float cameraAngle;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(cameraAngle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // LERP to the player's position + offset values
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(player.GetComponent<Player>().playerPosition.x,
                player.GetComponent<Player>().playerPosition.y + verticalFollowOffset,
                player.GetComponent<Player>().playerPosition.z + horizontalFollowOffset),
            followSpeed);
    }
}
