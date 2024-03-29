﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;

    public GameObject bulletManager;

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
            player.GetComponent<Player>().direction += Vector3.forward;
            IncreaseVelocity();
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.GetComponent<Player>().direction += -Vector3.right;
            IncreaseVelocity();
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.GetComponent<Player>().direction += -Vector3.forward;
            IncreaseVelocity();
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.GetComponent<Player>().direction += Vector3.right;
            IncreaseVelocity();
        }

        // Slow the player down by the deacceleration multiplier
        player.GetComponent<Player>().velocity *=
            player.GetComponent<Player>().deaccelerationMultiplier;

        // Normalize the player's direction vector
        player.GetComponent<Player>().direction.Normalize();

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
                if (Input.GetKeyDown(KeyCode.Space))
                    player.GetComponent<Player>().submergeState = SubmergeState.Submerging;

                if (Input.GetMouseButtonDown(0))
                    bulletManager.GetComponent<BulletManager>().FireBullet();

                break;

            case SubmergeState.Submerging:
                if(player.GetComponent<Player>().playerPosition.y > player.GetComponent<Player>().submergeDepth)
                {
                    player.GetComponent<Player>().playerPosition.y -= player.GetComponent<Player>().submergeSpeed;
                }
                else
                {
                    player.GetComponent<Player>().playerPosition.y = player.GetComponent<Player>().submergeDepth;
                    player.GetComponent<Player>().submergeState = SubmergeState.Submerged;
                }
                break;

            case SubmergeState.Submerged:
                if (Input.GetKeyDown(KeyCode.Space))
                    player.GetComponent<Player>().submergeState = SubmergeState.Surfacing;
                break;

            case SubmergeState.Surfacing:
                if (player.GetComponent<Player>().playerPosition.y < player.GetComponent<Player>().surfaceHeight)
                {
                    player.GetComponent<Player>().playerPosition.y += player.GetComponent<Player>().submergeSpeed;
                }
                else
                {
                    player.GetComponent<Player>().playerPosition.y = player.GetComponent<Player>().surfaceHeight;
                    player.GetComponent<Player>().submergeState = SubmergeState.Surfaced;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.GetComponent<Player>().submergeState = SubmergeState.Uppercut;
                    player.GetComponent<Player>().uppercutFX.GetComponent<ParticleSystem>().Play();
                }
                break;

            case SubmergeState.Uppercut:
                if (player.GetComponent<Player>().playerPosition.y < player.GetComponent<Player>().uppercutHeight)
                {
                    player.GetComponent<Player>().playerPosition.y += player.GetComponent<Player>().uppercutSpeed;
                }
                else
                {
                    player.GetComponent<Player>().submergeState = SubmergeState.UppercutEndLag;
                }
                break;

            case SubmergeState.UppercutEndLag:
                if (player.GetComponent<Player>().playerPosition.y > player.GetComponent<Player>().surfaceHeight)
                {
                    player.GetComponent<Player>().playerPosition.y -= player.GetComponent<Player>().uppercutSpeed;
                }
                else
                {
                    player.GetComponent<Player>().playerPosition.y = player.GetComponent<Player>().surfaceHeight;
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
