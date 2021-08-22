using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;

    private Rigidbody enemyRb;
    private GameObject player;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Character");
    }


    void Update()
    {
        MoveEnemy();
    }

    //Move logic
    virtual protected void MoveEnemy()
    {
        Vector3 target = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(target * speed);
    }


    //Figure out if object collided with something
    virtual protected void CollisionDetection()
    {

    }

    //Do something once it collided
    virtual protected void EnemyAction()
    {

    }
}
