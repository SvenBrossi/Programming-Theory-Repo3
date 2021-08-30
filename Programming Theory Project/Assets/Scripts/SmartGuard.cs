using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class SmartGuard : Enemy
{
    //private Rigidbody enemyRb;
    private GameObject player;

    [Header("Guard Settings")]
    [Tooltip("Enter 'left', 'right', 'up' or 'down' as parameters.")]
    public string direction = "up";

    private Vector3 startPosition;
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");

        startPosition = this.transform.position;
        ComputeEndPosition();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemyBackAndForth();
    }

    void MoveEnemyBackAndForth()
    {
        if (flipPos)
        {
            if (this.transform.position == startPosition)
            {
                ComputeEndPosition();
            }
            else
            {
                endPosition = startPosition;
            }
        }

        //INHERITANCE
        if (TargetInRange(searchRange, player.transform.position, transform.position))
        {
            Vector3 target = (player.transform.position - transform.position).normalized;
            this.GetComponent<Rigidbody>().AddForce(target * speed);
        }
        else
        {
            //INHERITANCE
            MoveEnemy(startPosition, endPosition);
        }
    }

    //Figure out if object collided with something
    override protected void OnCollisionEnter(Collision collision)
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

    protected void ComputeEndPosition()
    {

        endPosition = startPosition;

        if (direction == "up")
        {
            endPosition.z += 10.0f;
        }
        else if (direction == "down")
        {
            endPosition.z -= 10.0f;
        }
        else if (direction == "right")
        {
            endPosition.x += 10.0f;
        }
        else
        {
            endPosition.x -= 10.0f;
        }
    }
}

