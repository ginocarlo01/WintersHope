using System.Collections;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    [SerializeField]
    private float invincibleTime = 1f;

    [SerializeField]
    private float invincibleAlphaColor = 0.5f; // Adjusted alpha value

    [SerializeField]
    private float upSpeedInvincible = 1f;

    [SerializeField]
    private bool invincibleState;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); // Full alpha (1f)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !invincibleState)
        {
            invincibleState = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, invincibleAlphaColor);
            playerMovement.UpgradeMoveSpeed(upSpeedInvincible);
            StartCoroutine(WaitToFill());
        }
    }

    private IEnumerator WaitToFill()
    {
        yield return new WaitForSeconds(invincibleTime);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); // Full alpha (1f)
        playerMovement.ReduceMoveSpeed(upSpeedInvincible);
        invincibleState = false;
    }
}
