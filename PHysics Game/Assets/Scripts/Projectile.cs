using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject shooter;
    private float speed = 5f;
    private Vector3 playerPosition;
    private Vector3 direction;
    private float origDistance;
    private bool canHurtShooter = false;
    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        direction = player.transform.position + Vector3.up;
        origDistance = Vector3.Distance(playerPosition, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        if (positions.Count > 15) {
            positions.RemoveAt(0);
        }
        positions.Add(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if(t && t.tag.ToLower()=="player")
        {
            player.GetComponent<Player>().ReduceLife();
        } else if (t) {
            if (t.gameObject.Equals(shooter)) {
                if (canHurtShooter) {
                    //t.gameObject.GetComponent<EnemyLogic>().destroy = true;
                }
            }
        }
    }

    public void InvertDirection(Vector3 normal) {
        Vector3 vector = positions[positions.Count - 1] - positions[0];
        direction = (origDistance + 2) * Vector3.Reflect(vector, normal);
        //float xRand = Random.Range(-2.0f, 2.0f);
        //float yRand = Random.Range(0.0f, 1.0f);
        //Vector3 shooterPos = shooter.transform.position;
        //direction = new Vector3(shooterPos.x + xRand, shooterPos.y + yRand, shooterPos.z);
    }

    public float GetOrigDistance() {
        return origDistance;
    }
}
