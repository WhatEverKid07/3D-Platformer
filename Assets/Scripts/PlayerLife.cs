using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;
    //bool dead = false;
    
   // private void Update()
    //{
     //  if(transform.position.y < -2f && !dead)
      //  {
       //     Die();
        //} 
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Body"))
        {
            Debug.Log("Dead");
            Die();
        }
    }

    private void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Debug.Log("Killed");
            GameObject.FindGameObjectsWithTag("Enemy");
            
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel),1.5f);
        //dead = true;
        deathSound.Play();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
