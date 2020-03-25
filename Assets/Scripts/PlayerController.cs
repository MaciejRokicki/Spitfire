using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera mainCamera;
    public float movementSpeed = 8.0f;

    public GameObject BulletSpawner;
    public GameObject BulletPrefab;

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
        float distance = Vector2.Distance(mousePosition, this.gameObject.transform.position);
        Vector2 destinationPosition = (mousePosition - (Vector2)transform.position).normalized;

        if(canChangeDirection && distance > 1.0f)
        {
            rb.velocity = destinationPosition * movementSpeed;
            lastMousePositionInDistance = mousePosition.normalized;
            
            transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - (Vector3)mousePosition);
        }

        if(lastMousePositionInDistance == mousePosition.normalized)
            canChangeDirection = false;
        else
            canChangeDirection = true;
            
        if(time >= 1.0f)
        {
            Shoot();
            time = 0.0f;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawner.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = -transform.up * 15.0f;
    }
}
