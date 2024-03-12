using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyModel; 
    public GameObject player;
    private List<GameObject> enemies = new List<GameObject>();
    public int amount = 5;
    private bool stop = false;

    void Start()
    {
    }

    void Update()
    {
        if (enemies.Count > 0) {
            for (int i = 0; i < enemies.Count; i++) {
                GameObject enemy = enemies[i];
                if (enemy.GetComponent<EnemyLogic>().destroy) {
                    enemies.RemoveAt(i);
                    Destroy(enemy);
                }
            }
        }
    }

    public void SpawnEnemiesHandler() {
        if (enemies.Count < 10) {
            for (int i = 0; i < amount; i++) {
                if (stop) {
                    return;
                }
                int x = Random.Range(-12, 12);
                int z = Random.Range(15, 20);    
                Vector3 position = new Vector3(x, 1.0f, z);
                
                GameObject instance = Instantiate(enemyModel, position, Quaternion.identity);
                instance.transform.LookAt(player.transform.position);
                instance.transform.Rotate(0.0f, 90.0f, 0.0f);
                instance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                instance.GetComponent<EnemyLogic>().player = player;
                instance.GetComponent<EnemyLogic>().active = true;
                enemies.Add(instance);
            }
        }
        
    }

    public void DestroyAll() {
        CancelInvoke();
        stop = true;
        for (int i = 0; i < enemies.Count; i++) {
            GameObject enemy = enemies[i];
            enemy.GetComponent<EnemyLogic>().DestroyAllProjectiles();
            Destroy(enemy);
            enemies.RemoveAt(i);
        }
    }

    public void SpawnEnemies() {
        InvokeRepeating("SpawnEnemiesHandler", 0.0f, 3.5f);
    }

    public int GetEnemyCount() {
        return enemies.Count;
    }

    public void SetStop(bool isStop) {
        stop = isStop;
    }
}
