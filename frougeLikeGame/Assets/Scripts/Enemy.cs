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
    public bool vertical = true;
    Vector3 origin;
    void Start()
    {
        origin = transform.position;
        player = GameObject.Find("Player Character").GetComponent<Player>();
        cannon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemyPosition;
        LookAt();
        if (vertical)
        {
            MoveVertical();
        }
        else
        {
            MoveHorizontal();
        }
    }

    private void LookAt()
    {
        playerVector = new Vector3(player.playerPosition.x, player.playerPosition.y, player.playerPosition.z) + player.velocity * 10;
        cannon.transform.LookAt(playerVector);
    }

    private void MoveVertical()
    {
        Vector3 maxDistance = new Vector3(origin.x, origin.y, origin.z - .5f);
        Vector3 minDistance = new Vector3(origin.x, origin.y, origin.z + .5f);
        float moveValue = .05f;
        if(enemyPosition.z > maxDistance.z)
        {
            moveValue = -1 * Mathf.Abs(moveValue);
        }
        if(enemyPosition.z < minDistance.z)
        {
            moveValue = Mathf.Abs(moveValue);
        }
        enemyPosition.z += moveValue * Time.deltaTime;
        Debug.Log("Max Distance: " + maxDistance + " min distance: " + minDistance + " enemy pos: " + enemyPosition.z + " move value: " + moveValue);
    }

    private void MoveHorizontal()
    {

    }

}
