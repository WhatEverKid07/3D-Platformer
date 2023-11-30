using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;

    public Animator myAnim;
 
    [SerializeField] PlayerMovement isAbleToKill;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body") && isAbleToKill.isAbleToKill == false)
        {
            Debug.Log("Dead");
            //Die();
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
        SceneManager.LoadScene("Death Scene");
    }

}
