using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField]
    private int health, damage;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(target.transform.position);
    }
}
