using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrength = 30.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup=true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

        }
    }   IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup=false;
    }
    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& hasPowerup)
        {
            Rigidbody enemyRigidbody= collision. gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collied with" + collision.gameObject.name + "withpowerup set to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength,ForceMode.Impulse);
        }
    }
}
