using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Joystick joystick;
    private Vector2 moveInput = Vector2.zero;

    public GameObject playerGameObject;
    private Player playerScript;

    [SerializeField]private bool isMobile = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerGameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobile)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
        }
        else
        {

            moveInput.x = joystick.Horizontal;
            moveInput.y = joystick.Vertical;
        }

        if (playerScript != null)
        {
            playerScript.Move(moveInput);

            if (Input.GetButtonDown("Action"))
            {
                playerScript.Action();
            }

            if (Input.GetButtonDown("Special"))
            {
                playerScript.Special();
            }
        }

    }

    public void A()
    {
        if (playerScript != null)
        {
            playerScript.Action();
        }
    }

    public void B()
    {
        if (playerScript != null)
        {
            playerScript.Special();
        }
    }
}
