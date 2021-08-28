using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;
    //public int numLives = 3;
    public float searchRange = 5.0f;

    private Rigidbody enemyRb;
    private GameObject player;

    //[Header("Enemy Guarding Perimeter")]
    //[Tooltip("Enter Vector3 type of Position")]
    //public Vector3 startPosition;
    //public Vector3 endPosition;

    protected bool flipPos = false;

    //Color32 objColor;
    //Color32 newColor;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Character");

    }


    void Update()
    {
        MoveEnemy();
    }

    //Enemy Move logic
    virtual protected void MoveEnemy()
    {
        Vector3 target = (player.transform.position - transform.position).normalized;

        if (TargetInRange(searchRange, player.transform.position, transform.position))
        {
            enemyRb.AddForce(target * speed);
        }
    }

    virtual protected void MoveEnemy(Vector3 originPos, Vector3 targetPos)
    {
        if (this.transform.position != targetPos)
        {
            transform.LookAt(targetPos);
            flipPos = false;
        }

        transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);

        if (this.transform.position == targetPos)
        {
            flipPos = true;
        }
    }


    public bool TargetInRange(float range, Vector3 pos1, Vector3 pos2)
    {
        bool targetInRange = false;

        float dist1 = DetermineDistance(pos1.x, pos2.x);
        float dist2 = DetermineDistance(pos1.z, pos2.z);

        if (dist1 < range && dist2 < range)
        {
            targetInRange = true;
        }

        return targetInRange;
    }

    public float DetermineDistance(float point1, float point2)
    {
        float distance = Mathf.Abs(point1 - point2);
        return distance;
    }

    //Figure out if object collided with something
    virtual protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Reduce players life
            //numLives--;
            player.GetComponent<PersonController>().UpdateLives(-1);

            // Actions the enemy performs when touching the player
            ChangeColor();
        }
    }

    //Do something once it collided
    virtual protected void ChangeColor()
    {
        Color32 newColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gameObject.GetComponent<MeshRenderer>().material.color = newColor;
    }
}