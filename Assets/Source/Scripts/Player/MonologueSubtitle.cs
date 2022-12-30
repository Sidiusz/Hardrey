using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonologueSubtitle : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;
    public float textSpeed = 1f;
    public List<string> monologueText;
    public GameObject player;

    private int textIndex = 0;
    private int charIndex = 0;
    private bool skippingText = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the trigger, turn off the player's control and show the panel
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().controlDisabled = true;
            player.GetComponent<PlayerController>().ResetVelocity();
            panel.SetActive(true);

            // Reset player speed to zero
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // Start the monologue coroutine
            StartCoroutine(PlayMonologue());
        }
    }

    IEnumerator PlayMonologue()
    {
        // While there is still text in the list
        while (textIndex < monologueText.Count)
        {
            // Reset the character index and skipping flag
            charIndex = 0;
            skippingText = false;

            // Get the current string of text
            string currentText = monologueText[textIndex];

            // While there are still characters in the string
            while (charIndex < currentText.Length)
            {
                // If the player is not skipping the text, add one character to the display
                if (!skippingText)
                {
                    text.text = currentText.Substring(0, charIndex + 1);
                }

                // Wait for the text speed time before adding the next character
                yield return new WaitForSeconds(1 / textSpeed);

                charIndex++;
            }

            // Wait for player input to advance to the next string
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                // If the player starts skipping the text, display the entire string
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    skippingText = true;
                    text.text = currentText;
                }

                yield return null;
            }

            textIndex++;
        }

        // When the text has ended, hide the panel and turn on the player's control
        panel.SetActive(false);
        player.GetComponent<PlayerController>().controlDisabled = false;
    }
}
