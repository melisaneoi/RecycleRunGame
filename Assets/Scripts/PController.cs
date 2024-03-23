using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public Animator animator;

    private Rigidbody rb;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                
        if (animator != null)
        {
            animator.SetBool("isRunning", movement.magnitude > 0);
        }
                
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {        
        MoveCharacter(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    }

    private void MoveCharacter(Vector3 direction)
    {
        if (direction.magnitude > 0)
        {
            rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("engel"))
        {
            gameManager.ChangeHealth(-1);
            Destroy(collision.gameObject);
        }        
        else if (collision.gameObject.CompareTag("OrganicWaste"))
        {
            gameManager.ChangeScore(1);
            Destroy(collision.gameObject);
        }        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("container"))
        {
            Debug.Log("Container ile temas edildi, oyun sonlanıyor.");            
            gameManager.EndGame();
        }
    }
}



