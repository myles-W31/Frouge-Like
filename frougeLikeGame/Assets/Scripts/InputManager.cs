using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;

    private Plane plane;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        SurfaceState();

        GetAngleToMouse();
    }

    public void Move()
    {
        // Add acceleration to velocity
        player.GetComponent<Player>().acceleration =
            player.GetComponent<Player>().rateOfAccel *
            player.GetComponent<Player>().direction;

        if (Input.GetKey(KeyCode.W))
        {
            player.GetComponent<Player>().direction = Vector3.forward;
            IncreaseVelocity();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            player.GetComponent<Player>().direction = -Vector3.right;
            IncreaseVelocity();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.GetComponent<Player>().direction = -Vector3.forward;
            IncreaseVelocity();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player.GetComponent<Player>().direction = Vector3.right;
            IncreaseVelocity();
        }
        else
        {
            player.GetComponent<Player>().velocity *=
                player.GetComponent<Player>().deaccelerationMultiplier;
        }

        // Move Player along velocity
        //velocity = speed * direction;

        // Limit vel vector with ClampMagnitude
        player.GetComponent<Player>().velocity =
        Vector3.ClampMagnitude(player.GetComponent<Player>().velocity,
        player.GetComponent<Player>().maxSpeed);

        // Add vel to position
        player.GetComponent<Player>().playerPosition +=
            player.GetComponent<Player>().velocity;
    }

    public void SurfaceState()
    {
        switch (player.GetComponent<Player>().submergeState)
        {
            case SubmergeState.Surfaced:
                if (Input.GetKey(KeyCode.Space))
                    player.GetComponent<Player>().submergeState = SubmergeState.Submerging;
                break;

            case SubmergeState.Submerging:
                if(player.GetComponent<Player>().playerPosition.y > player.GetComponent<Player>().submergeDepth)
                {
                    player.GetComponent<Player>().playerPosition.y -= player.GetComponent<Player>().submergeSpeed;
                }
                else
                {
                    player.GetComponent<Player>().submergeState = SubmergeState.Submerged;
                }
                break;

            case SubmergeState.Submerged:
                if (Input.GetKey(KeyCode.Space))
                    player.GetComponent<Player>().submergeState = SubmergeState.Surfacing;
                break;

            case SubmergeState.Surfacing:
                if (player.GetComponent<Player>().playerPosition.y < player.GetComponent<Player>().surfaceHeight)
                {
                    player.GetComponent<Player>().playerPosition.y += player.GetComponent<Player>().submergeSpeed;
                }
                else
                {
                    player.GetComponent<Player>().submergeState = SubmergeState.Surfaced;
                }
                break;
        }
    }

    public void GetAngleToMouse()
    {
        // Raycast the mouse positon and find the angle of rotation from the player
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - player.GetComponent<Player>().playerPosition;
            player.GetComponent<Player>().angleOfRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // player.GetComponent<Player>().transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    // Helper function to increase the player's velocity by adding acceleration
    public void IncreaseVelocity()
    {
        player.GetComponent<Player>().velocity +=
            player.GetComponent<Player>().acceleration;
    }
}
