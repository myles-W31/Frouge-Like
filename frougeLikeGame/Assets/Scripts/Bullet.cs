using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // The bullet class (at least how I imagine) will be a container of 
    // individual bullet information. The bulletManager class will perform
    // the function of all the bullets every frame.

    public Vector3 bulletPos;
    public Vector3 bulletDir;
    public Vector3 bulletVel;
}
