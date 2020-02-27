using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //get reference to the child gameobject that will be rotating
    GameObject cannon;
    //get reference to player
    Player player;
    // Start is called before the first frame update
    public Vector3 enemyPosition = new Vector3(2.5f, 0, -1.75f);
    public Vector3 playerVector;
    void Start()
    {
        player = GameObject.Find("Player Character").GetComponent<Player>();
        cannon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemyPosition;
        LookAt();
    }

    private void LookAt()
    {
        playerVector = new Vector3(player.playerPosition.x, player.playerPosition.y, player.playerPosition.z) + player.velocity * 10;
        cannon.transform.LookAt(playerVector);
        //Debug.DrawLine(transform.position, playerVector);
    }
}
