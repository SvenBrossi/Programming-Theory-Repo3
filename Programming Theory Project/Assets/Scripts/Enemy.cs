using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;
    public int numLives = 3;

    private Rigidbody enemyRb;
    private GameObject player;

    //Color32 objColor;
    Color32 newColor;

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

    virtual protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player: " + collision.gameObject.name);
            numLives--;
            Debug.Log("Number of Lives: " + numLives);
            EnemyAction();
        }
        else
        {
            Debug.Log("Collided with object: " + collision.gameObject.name);
        }
    }

    //Do something once it collided
    virtual protected void EnemyAction()
    {
        //GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //objColor = gameObject.GetComponent<MeshRenderer>().material.color;
        //Debug.Log("Enemy Color: " + objColor);
        newColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gameObject.GetComponent<MeshRenderer>().material.color = newColor;
    }
}