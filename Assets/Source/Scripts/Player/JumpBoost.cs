using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float boostDuration = 3f; 
    private bool boostAvailable = true; 

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && boostAvailable)
        {
            other.gameObject.GetComponent<PlayerController>().canAirJump = true;
            boostAvailable = false;
            StartCoroutine(BoostDurationTimer());
        }
    }
    IEnumerator BoostDurationTimer()
    {
        yield return new WaitForSeconds(boostDuration);
        boostAvailable = true;
    }
}