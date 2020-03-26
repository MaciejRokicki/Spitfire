using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera mainCamera;
    public float movementSpeed = 8.0f;
    public bool canShoot = true;
    public GameObject BulletSpawner;
    public GameObject BulletPrefab;
    public float bulletSpeed = 20.0f;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Start()
    {

    }

    private float time = 0.0f;
    private bool canChangeDirection = true;
    private Vector2 lastMousePositionInDistance;

    void Update()
    {
        time += Time.deltaTime;

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePositionNormalizde = mousePosition.normalized;
        float distance = Vector2.Distance(mousePosition, this.gameObject.transform.position);
        Vector2 destinationPosition = (mousePosition - (Vector2)transform.position).normalized;

        if(canChangeDirection && distance > 1.0f)
        {
            rb.velocity = destinationPosition * movementSpeed;
            lastMousePositionInDistance = mousePositionNormalizde;
            
            this.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - (Vector3)mousePosition);
        }

        float changeMousePosition = Vector2.Distance(lastMousePositionInDistance, mousePositionNormalizde);

        if(changeMousePosition <= 0.05f)
            canChangeDirection = false;
        else
            canChangeDirection = true;
            
        if(canShoot && time >= 0.5f)
        {
            Shoot();
            time = 0.0f;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawner.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = -transform.up * bulletSpeed;
    }
}
