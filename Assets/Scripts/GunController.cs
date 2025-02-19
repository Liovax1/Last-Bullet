using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;  // Sph�re � tirer
    public Transform muzzle;         // Point de sortie des balles
    public float bulletSpeed = 50f;  // Vitesse des balles
    public float fireRate = 0.1f;    // Cadence de tir
    public float recoilForce = 0.1f; // Effet de recul
    public Camera playerCamera;      // Cam�ra du joueur
    public Vector3 aimPosition;      // Position de vis�e
    public Vector3 hipPosition;      // Position normale
    public float aimSpeed = 10f;     // Vitesse de transition entre vis�e et normal
    public AudioClip gunshotSound;  // Son du tir
    private AudioSource audioSource;  // Référence à l'AudioSource


    private bool isAiming = false;
    private float nextFireTime = 0f;

    void Start()
    {
        hipPosition = transform.localPosition; // Position initiale de l'arme
        audioSource = GetComponent<AudioSource>();  // Récupère l'AudioSource de l'arme
    }

    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    void HandleAiming()
    {
        if (Input.GetMouseButton(1)) // Clic droit pour viser
        {
            isAiming = true;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aimSpeed);
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 40, Time.deltaTime * aimSpeed); // Zoom cam�ra
        }
        else
        {
            isAiming = false;
            transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * aimSpeed);
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60, Time.deltaTime * aimSpeed); // Retour FOV normal
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Clic gauche pour tirer
        {
            nextFireTime = Time.time + fireRate;

            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = muzzle.forward * bulletSpeed; // Direction de tir

        // Vérifier si la balle touche un ennemi avec un Raycast
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, 100f))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Inflige 1 dégât
            }
        }

        // Appliquer un recul dynamique basé sur l'orientation du joueur
        Vector3 recoilDirection = -transform.forward;
        transform.localPosition += recoilDirection * recoilForce;

        // Jouer le son du tir
        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }


}