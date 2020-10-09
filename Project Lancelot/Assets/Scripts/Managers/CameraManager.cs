using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Variables

    [Tooltip("The intended target for the camera to follow.")] public Player target; //The intended target for the camera to follow

    [Tooltip("The speed at which the camera smoothes back to the player.")] public float smoothSpeed = 7.5f; //The speed at which the camera smoothes

    public float followingDistance = 1.5f;

    [Tooltip("The amount of distance ahead of the target is when moving.")] public float leadDistance = 1.5f; //The amount of distance ahead of the target is when moving.

    [Tooltip("The offset amount on the X-axis used to determine if the camera is in range of the target.")] public float offset_x = .05f; //The offset amount on the X-axis used to determine if the camera is in range of the target.

    [Tooltip("The offset amount on the Y-axis used to determine if the camera is in range of the target.")] public float offset_y = .03f; //The offset amount on the Y-axis used to determine if the camera is in range of the target.

    private bool onTarget = false; //The private variable used for state checking;

    [Tooltip("The animator used for the camera.")] public Animator camAnim; //The animator for the camera

    [Tooltip("The instance of the camera.")] public static CameraManager instance; // The instance of the camera

    private void Awake()
    {
        //Creates an instance of CameraController if one does not exist, if one does exist, it destroys itself
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>();
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Finds our target, the player, if our target is null


        if (target != null)
        {
            if (Vector2.Distance(this.transform.position, target.transform.position) >= followingDistance && onTarget)
            {
                onTarget = false;
            }

            if (!onTarget)
            {
                if (target.movement != Vector2.zero)
                {
                    Vector3 predictedPosition = new Vector3(target.movement.x, target.movement.y, 0).normalized;
                    predictedPosition = (predictedPosition * leadDistance) + target.transform.position;
                    this.transform.position = Vector2.Lerp(transform.position, predictedPosition, smoothSpeed * Time.deltaTime);

                    if (this.transform.position.x >= predictedPosition.x - offset_x && this.transform.position.x <= predictedPosition.x + offset_x && this.transform.position.y >= predictedPosition.y - offset_y && this.transform.position.y <= predictedPosition.y + offset_y)
                    {
                        onTarget = true;
                    }
                }
                else
                {
                    this.transform.position = Vector2.Lerp(transform.position, target.transform.position, smoothSpeed * 2 * Time.deltaTime);

                    if (this.transform.position.x >= target.transform.position.x - offset_x && this.transform.position.x <= target.transform.position.x + offset_x && this.transform.position.y >= target.transform.position.y - offset_y && this.transform.position.y <= target.transform.position.y + offset_y)
                    {
                        onTarget = true;
                    }

                }

            }
        }
    }

    public void Shake()
    {
        int rand = Random.Range(0,3);

        if (rand == 0)
        {
            camAnim.SetTrigger("Shake");
            return;
        }
        if (rand == 1)
        {
            camAnim.SetTrigger("Shake2");
            return;
        }
        if (rand == 2)
        {
            camAnim.SetTrigger("Shake3");
            return;
        }
        
    }

}


