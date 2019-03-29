using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    public float speed;
    public float huntSpeed;
    public List<Transform> wayPoints = new List<Transform>();
    public Transform player;

    public static float damage = 20f;

    private int cPoint = 0;
    private int pointSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        pointSize = wayPoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // handle lifeCycle Game
        if (Constants.gameState == Constants.GameState.Play)
        {
            // handle enemy movement
            handleMovement();
        }
    }

    /**
     * handle enemy movement function
     */
    private void handleMovement ()
    {
        // get distance from enemy to player
        Vector3 playerDir = player.position - transform.position;

        // check distance between player and enemy
        if (playerDir.magnitude >= 15f || Constants.playerHited)
        {
            // check distance between enemy and one of points
            if (pointSize > 0)
            {
                // get distance from enemy to point
                Vector3 dir = wayPoints[cPoint].position - transform.position;

                if (dir.magnitude < 0.5f)
                {
                    // changing direction point
                    cPoint = (cPoint + 1) % pointSize;

                    // set player hitted
                    Constants.playerHited = false;
                }
                else
                {
                    // moving enemy
                    transform.position += dir.normalized * speed * Time.deltaTime;
                }

                // call handle rotation
                handleRotation(dir);
            }
        } else
        {
            // moving enemy to player
            transform.position += playerDir.normalized * huntSpeed * Time.deltaTime;

            // call handle rotation
            handleRotation(playerDir);
        }
    }

    /**
     * handle enemy rotation
     */
     private void handleRotation( Vector3 dir)
     {
        // initializing angle position
        float angle = dir.x < 0 ? 0f : 180f;

        // rotating enemy
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
     }
}
