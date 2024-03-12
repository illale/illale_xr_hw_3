using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject projectile;
    public GameObject cannon;
    private float speed = 0.5f;
    public bool destroy = false;
    public bool active = false;
    public bool shooting = false;
    private List<GameObject> projectiles = new List<GameObject>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (!shooting) {
                float time = Random.Range(4.0f, 10.0f);
                InvokeRepeating("LaunchProjectile", 0.0f, time);
                shooting = true;
            } else {
                for (int i = 0; i < projectiles.Count; i++) {
                    GameObject projectileInstace = projectiles[i];
                    float dist = Vector3.Distance(player.transform.position, projectileInstace.transform.position);
                    if (dist > projectileInstace.GetComponent<Projectile>().GetOrigDistance()) {
                        projectiles.RemoveAt(i);
                        Destroy(projectileInstace);
                    }
                }
            }

        }
    }

    private void LaunchProjectile() 
    {
        GameObject instance = Instantiate(projectile);
        projectiles.Add(instance);
        instance.transform.position = cannon.transform.position;
        instance.GetComponent<Projectile>().player = player;
        instance.tag = "Projectile";
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
        {
            destroy = true;
            CancelInvoke();
            player.GetComponent<Player>().UpdateScore();
            DestroyAllProjectiles();
        } else if (t && t.tag.ToLower()=="player") {
            destroy = true;
            CancelInvoke();
            player.GetComponent<Player>().ReduceLife();
            DestroyAllProjectiles();
        }
        
    }

    public void DestroyAllProjectiles() {
        for (int i = 0; i < projectiles.Count; i++) {
                GameObject projectileInstace = projectiles[i];
                Destroy(projectileInstace);
        }
    }
}
