using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject player;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float cullDistance;

    public List<GameObject> bulletList;

    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBullets();

        DestroyBullets();
    }

    void UpdateBullets()
    {
        foreach(GameObject b in bulletList)
        {
            b.GetComponent<Bullet>().bulletPos += 
                b.GetComponent<Bullet>().bulletVel;

            b.GetComponent<Bullet>().transform.position = 
                b.GetComponent<Bullet>().bulletPos;
        }
    }

    void DestroyBullets()
    {
        for(int i = 0; i < bulletList.Count; i++)
        {
            // probably check for y axis being below the floor maybe?
            // that is if these bullets are effected by gravity

            // For now I'm going to check with distance but I think that
            // is expensive so it will likely be time based in the future.
            if(Vector3.Distance(player.GetComponent<Player>().playerPosition,
                bulletList[i].GetComponent<Bullet>().bulletPos) > cullDistance)
            {
                Destroy(bulletList[i]);
                bulletList.Remove(bulletList[i]);

                i--;
            }
        }
    }

    public void FireBullet()
    {
        // Instantiate the new bullet under the swamp so it isn't seen too early
        GameObject newBullet = Instantiate(bulletPrefab,
            new Vector3(0, -10, 0),
            Quaternion.identity);

        // Set the new bullet's position to that of th player's current position
        newBullet.GetComponent<Bullet>().bulletPos =
            new Vector3(player.GetComponent<Player>().playerPosition.x,
                player.GetComponent<Player>().playerPosition.y - 0.055f,
                player.GetComponent<Player>().playerPosition.z);

        // Create a normalized velocity vector using the player's angle of rotation
        // Then multiply it by the set bullet speed
        Vector3 bulletVelocity = 
            new Vector3(Mathf.Sin(player.GetComponent<Player>().angleOfRotation * Mathf.Deg2Rad), 0,
                Mathf.Cos(player.GetComponent<Player>().angleOfRotation * Mathf.Deg2Rad));

        bulletVelocity.Normalize();

        newBullet.GetComponent<Bullet>().bulletVel = bulletVelocity * bulletSpeed;

        // Add new bullet to the list
        bulletList.Add(newBullet);
    }
}
