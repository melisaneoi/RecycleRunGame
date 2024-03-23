using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    public int score = 0;
    public int can = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OrganicWaste")
        {
            score++;
            Debug.Log(score + " : skorunuz");
            Destroy(other.gameObject);
        }
        else if (other.tag == "engel")
        {
            if (can > 0)
            {
                can--;
                Debug.Log(can + " : canınız kaldı");
                Destroy(other.gameObject);
            }
        }
        else if (other.tag == "container")
        {
            if (score >= 30)
            {
                Debug.Log("Tebrikler, görevi tamamladınız! Artık 2. seviyeye geçebilirsiniz.");
            }
            else
            {
                Debug.Log("Maalesef sokağımız yeterince temiz değil.Tekrar deneyiniz.");
            }

            // Oyunu durdur
            Time.timeScale = 0f;
        }

        // Can kalmadıysa oyunu durdur
        if (can <= 0)
        {
            Time.timeScale = 0f;
        }
    }
}
