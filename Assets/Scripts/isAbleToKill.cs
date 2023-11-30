using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAbleToKill : MonoBehaviour
{

    public bool attacking;
    public Collider KillBox;
    public PlayerMovement Player;
    public int AppearTime;

    // Update is called once per frame
    void Update()
    {
        KillBox.enabled = (AppearTime > 0);

        if (Player.isAbleToKill)
        {
            AppearTime = 200;
        }

        AppearTime -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Enemy Body"))
        {
            Debug.Log("Killed");
            other.gameObject.SetActive(false);
        }
    }
    
}
