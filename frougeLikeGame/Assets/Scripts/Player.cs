using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SubmergeState
{
    Surfaced,
    Surfacing,
    Submerged,
    Submerging,
    Uppercut,
    UppercutEndLag
}

public class Player : MonoBehaviour
{
    // Alter these to modify movement properties
    public float rateOfAccel;
    public float maxSpeed;
    public float rotationSpeed;
    public float angleOfRotation;
    public float deaccelerationMultiplier;

    // DO NOT TOUCH - altered via Update()
    public Vector3 playerPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);

    // Submerge SM
    public SubmergeState submergeState;
    // Height while on the surface
    public float surfaceHeight;
    // Depth when using dive ability
    public float submergeDepth;
    // Speed at which the player dives and surfaces
    public float submergeSpeed;

    // The height to which the player rises when performing an uppercut 
    public float uppercutHeight;
    // The speed at which the player performs an uppercut attack
    public float uppercutSpeed;

    // Uppercut splash particle FX prefab
    public GameObject uppercutFXPrefab;
    public GameObject uppercutFX;

    // Start is called before the first frame update
    void Start()
    {
        submergeState = SubmergeState.Surfaced;

        uppercutFX = Instantiate(uppercutFXPrefab,
            playerPosition, Quaternion.Euler(-90, 0, 180));
    }

    // Update is called once per frame
    void Update()
    {
        SetTransform();
    }

    public void SetTransform()
    {
        // Rotate the vehicle to face toawared the mouse pointer
        transform.rotation = Quaternion.Euler(0, angleOfRotation, 0);

        // Debug.Log(angleOfRotation);
        // Position vehicle
        transform.position = playerPosition;

        // Set the FX system to the player's current position
        uppercutFX.transform.position = playerPosition;
    }
}
