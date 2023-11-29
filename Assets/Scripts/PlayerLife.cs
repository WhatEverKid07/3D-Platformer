using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;

    public Animator myAnim;

    private void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Body"))
        {
            Debug.Log("Dead");
            Die();
        }
        if (collision.gameObject.CompareTag("Enemy Body"))
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
        Invoke(nameof(ReloadLevel),1f);
        deathSound.Play();
        myAnim.SetTrigger("Dead");
        Debug.Log("Dead");
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("End Scene");
    }
}
