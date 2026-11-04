using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public Animator gateAnimator;
    public float speed = 5f;
    public float jumpForce = 300f;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI messageText;
    public AudioClip coinClip;

    private Rigidbody rb;
    private AudioSource src;
    private Shooter shooter;
    private Vector2 move;
    private bool fireHeld;
    private PlayerInput playerInput;
    private bool onGround;
    private int coins;
    private int totalCoins;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        src = GetComponent<AudioSource>();
        shooter = GetComponent<Shooter>();
        playerInput = GetComponent<PlayerInput>();

        Time.timeScale = 1f;
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        if (coinText != null) coinText.text = "Монеты " + coins + " / " + totalCoins;
        if (messageText != null) messageText.text = "";
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        if (shooter == null) shooter = GetComponent<Shooter>();
        if (shooter != null) shooter.SetAim(move);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && onGround && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce);
            onGround = false;
        }
    }

    public void OnFire(InputValue value)
    {
        fireHeld = value.isPressed;
    }

    private void Update()
    {
        bool firePressed = playerInput != null && playerInput.actions["Fire"].IsPressed();
        if (firePressed && shooter != null) shooter.Shoot();

        if (transform.position.y < -10f) Restart();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.y * speed);
        onGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            if (coinText != null) coinText.text = "Монеты " + coins + " / " + totalCoins;
            if (src != null && coinClip != null) src.PlayOneShot(coinClip);
        }

        if (other.CompareTag("Exit"))
        {
            if (gateAnimator != null && gateAnimator.GetBool("Open"))
            {

                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    Debug.LogWarning("Next level is not added to Build Settings!");
                }

            }
            else
            {
                if (messageText != null) messageText.text = "Survive until the gate opens!";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Exit") && Time.timeScale > 0f && messageText != null)
            messageText.text = "";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) Restart();
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f)
            {
                onGround = true;
                return;
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
