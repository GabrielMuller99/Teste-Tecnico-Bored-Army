using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    [SerializeField] HealthSystem healthBar;

    [SerializeField] Pooling bigExplosionPooling;
    [SerializeField] Pooling mediumExplosionPooling;
    [SerializeField] GameplayManager gameManager;

    [SerializeField] Sprite fullHealthSprite;
    [SerializeField] Sprite midHealthSprite;
    [SerializeField] Sprite lowHealthSprite;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            switch (gameObject.tag)
            {
                case "Player":
                    ExplosionEffect();
                    break;

                case "Shooter":
                    gameManager.Score(10);

                    gameObject.SetActive(false);

                    ExplosionEffect();

                    break;

                case "Chaser":
                    gameManager.Score(5);

                    gameObject.SetActive(false);

                    ExplosionEffect();

                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (gameObject.tag)
            {
                case "Player":
                    if (currentHealth == 4)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = midHealthSprite;
                    }
                    else if (currentHealth == 2)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = lowHealthSprite;
                    }
                    else if (currentHealth == 6)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = fullHealthSprite;
                    }

                    break;

                case "Shooter":
                    if (currentHealth == 4)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = midHealthSprite;
                    }
                    else if (currentHealth == 2)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = lowHealthSprite;
                    }
                    else if (currentHealth == 6)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = fullHealthSprite;
                    }

                    break;

                case "Chaser":
                    if (currentHealth == 2)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = midHealthSprite;
                    }
                    else if (currentHealth == 1)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = lowHealthSprite;
                    }
                    else if (currentHealth == 3)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = fullHealthSprite;
                    }

                    break;

                default:
                    break;
            }
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        GameObject newExplosion = mediumExplosionPooling.GetObject();
        newExplosion.transform.position = transform.position;
        newExplosion.transform.rotation = transform.rotation;
        newExplosion.SetActive(true);
    }

    void ExplosionEffect()
    {
        GameObject newExplosion = bigExplosionPooling.GetObject();
        newExplosion.transform.position = transform.position;
        newExplosion.transform.rotation = transform.rotation;
        newExplosion.SetActive(true);
    }
}
