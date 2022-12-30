using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraJumpTrigger : MonoBehaviour
{
    public PlayerController playerController;
    public float disableDuration = 3f;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerController.jumpsLeft++;
            playerController.canAirJump = true;
            StartCoroutine(DisableTrigger());
        }
    }

    IEnumerator DisableTrigger()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(disableDuration);
        gameObject.SetActive(true);
    }
}
