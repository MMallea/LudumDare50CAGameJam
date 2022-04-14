using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Animator playerAnim;

    private float speedMultiplier = 1;
    private MovementActions movement;
    private bool front, back, left, right, space;
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
        movement.Keyboard.Accelerate.started += context => { space = true; };
        movement.Keyboard.Accelerate.canceled += context => { space = false; };
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
            transform.position += Vector3.left * Time.deltaTime * moveSpeed * speedMultiplier;
        }

        if (right)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed * speedMultiplier;
        }

        if (front)
        {
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed * speedMultiplier;
        }

        if (back)
        {
            transform.position += Vector3.back * Time.deltaTime * moveSpeed * speedMultiplier;
        }

        if (space)
        {
            transform.position += Vector3.down * Time.deltaTime * (moveSpeed * 2);
        }

        if (playerAnim != null)
        {
            playerAnim.SetBool("Left", left);
            playerAnim.SetBool("Right", right);
            playerAnim.SetBool("Dive", space);
        }

        speedMultiplier = space ? 0.5f : 1;
        if(space && GameManager.Instance != null && !GameManager.Instance.IsRumbling())
            GameManager.Instance.SetRumble(0.25f, 0.025f);

        if (SoundManager.Instance != null)
            SoundManager.Instance.windSource.volume = space ? 0.5f : 0.25f;
    }
}
