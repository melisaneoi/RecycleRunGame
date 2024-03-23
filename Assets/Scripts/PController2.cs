using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController2 : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 5f;
    public Animator animator;

    private Rigidbody rb;
    private GameManager2 gameManager2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        gameManager2 = FindObjectOfType<GameManager2>(); // Oyun yöneticisi olarak GameManager2'yi arayın ve referansını alın.
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
        // Bu kısımda etiketlere göre çarpışmayı kontrol etmek için yeni atıkların etiketlerini kontrol ediyoruz.
        if (collision.gameObject.CompareTag("engel"))
        {
            gameManager2.ChangeHealth(-1); // gameManager yerine gameManager2'nin ilgili fonksiyonunu çağırın.
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("PaperWaste"))
        {
            gameManager2.ChangePaperScore(1); // GameManager2 içinde Paper Score'u değiştiren yeni bir fonksiyon olmalı.
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("GlassWaste"))
        {
            gameManager2.ChangeGlassScore(1); // GameManager2 içinde Glass Score'u değiştiren yeni bir fonksiyon olmalı.
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("KonserveAtik"))
        {
            gameManager2.ChangeCanScore(1); // GameManager2 içinde Can Score'u değiştiren yeni bir fonksiyon olmalı.
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Oyun sonlanma koşulunu kontrol etmek için GameManager2 referansını kullanın.
        if (other.CompareTag("container"))
        {
            Debug.Log("Container ile temas edildi, oyun sonlanıyor.");
            gameManager2.EndGame(); // GameManager2 içindeki EndGame fonksiyonunu çağırın.
        }
    }
}

