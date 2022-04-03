using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Animator playerAnim;
    private MovementActions movement;
    private bool front, back, left, right;
    private void Awake()
    {
        front = false;
        left = false;
        right = false;
        movement = new MovementActions();
        movement.Keyboard.Left.started += context => { left = true; };
        movement.Keyboard.Left.canceled += context => { left = false; };
        movement.Keyboard.Right.started += context => { right = true; };
        movement.Keyboard.Right.canceled += context => { right = false; };
        movement.Keyboard.Front.started += context => { front = true; };
        movement.Keyboard.Front.canceled += context => { front = false; };
        movement.Keyboard.Back.started += context => { back = true; };
        movement.Keyboard.Back.canceled += context => { back = false; };
    }

    private void OnEnable()
    {
        movement.Keyboard.Enable();
    }

    private void OnDisable()
    {
        movement.Keyboard.Enable();
    }

    private void Update()
    {
        if (left)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }

        if (right)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }

        if (front)
        {
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        }

        if (back)
        {
            transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        }

        if(playerAnim != null)
        {
            playerAnim.SetBool("Left", left);
            playerAnim.SetBool("Right", right);
        }
    }
}
