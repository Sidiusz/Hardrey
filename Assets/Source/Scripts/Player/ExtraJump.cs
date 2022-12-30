using UnityEngine;

public class ExtraJump : MonoBehaviour
{
    public float pickupDuration = 3f;

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.canAirJump = true;
            gameObject.SetActive(false);
            Invoke("Activate", pickupDuration);
        }
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
}