using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float cullDistance;

    public List<GameObject> bulletList;

    //variable to keep track of time for delayed shooting
    private float time = 0.0f;
    //delay for enemy to shoot
    private float delay = 1;

    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBullets();
        EnemyBullet();
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

    private void EnemyBullet()
    {
        time += Time.deltaTime;
        if(time >= delay)
        {
            time = 0.0f;
            //instantiating new bullet, under swamp so it's not too early (good idea Sam c: )
            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(0, -10, 0), Quaternion.identity);
            //using sam's code for now, might refactor so that it's not as long, will also make it so it has list of enemys instead of just 1
            newBullet.GetComponent<Bullet>().bulletPos = new Vector3(enemy.GetComponent<Enemy>().enemyPosition.x,
                enemy.GetComponent<Enemy>().enemyPosition.y - .055f,
                enemy.GetComponent<Enemy>().enemyPosition.z);

            Vector3 bulletVel = new Vector3(Mathf.Sin(enemy.transform.GetChild(0).transform.rotation.y * Mathf.Deg2Rad), 0, enemy.transform.GetChild(0).transform.rotation.y * Mathf.Deg2Rad);
            bulletVel.Normalize();
            newBullet.GetComponent<Bullet>().bulletVel = bulletVel * bulletSpeed;

            bulletList.Add(newBullet);
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
