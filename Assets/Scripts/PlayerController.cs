using UnityEngine;
using UnityEngine.UI;  // Pour l'UI

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float lookSpeedX = 10f;
    public float lookSpeedY = 8f;
    public float jumpForce = 1f;
    public float gravity = -9.81f;
    private Animator animator;

    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    private float rotationX = 0f;

    public int score = 0;  // Variable pour le score
    public Text scoreText;  // R�f�rence au texte de l'UI pour afficher le score

    void Start()
    {
        animator = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>(); // R�cup�re la cam�ra enfant
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'�cran
        Cursor.visible = false; // Masque le curseur
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        MovePlayer();
        LookAround();
        Jump();
        UpdateScore();  // Met � jour l'affichage du score
    }

    void MovePlayer()
    {
        // D�terminer si on marche ou court
        float speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? sprintSpeed : moveSpeed;

        // Mouvement horizontal et avant/arri�re
        float moveDirectionX = Input.GetAxis("Horizontal") * speed;
        float moveDirectionZ = Input.GetAxis("Vertical") * speed;

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        // Appliquer la gravit�
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // Reste au sol quand on est au sol

        velocity.y += gravity * Time.deltaTime;
        velocity = new Vector3(move.x, velocity.y, move.z);

        // Appliquer le mouvement
        characterController.Move(velocity * Time.deltaTime);

        // Mettre � jour l'animation apr�s le mouvement
        float currentSpeed = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }

    void LookAround()
    {
        // Rotation de la cam�ra en fonction de la souris
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Rotation de la cam�ra verticalement
        transform.Rotate(Vector3.up * mouseX); // Rotation du personnage horizontalement
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            animator.SetBool("IsJumping", true);
        }

        // Mettre � jour l'animation de chute
        animator.SetBool("IsFalling", !isGrounded);
        animator.SetBool("IsJumping", isGrounded ? false : animator.GetBool("IsJumping"));
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;  // Met � jour l'affichage du score � l'�cran
        }
    }
}
