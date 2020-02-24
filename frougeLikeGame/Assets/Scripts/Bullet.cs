using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // The bullet class (at least how I imagine it) will be a container of 
    // individual bullet information. The bulletManager class will perform
    // the function of all the existing bullets every frame.

    public Vector3 bulletPos;
    public Vector3 bulletVel;

    // Direction only required if we use a non-spherical bullet
    public Vector3 bulletDir;
}
