using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileLifeSpan;
    public int damage;

    void OnEnable()
    {
        // Invoke("Deactivate", projectileLifeSpan);
        StartCoroutine(Deactivate());
    }

    void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime * transform.up, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthController>() != null)
        {
            collision.gameObject.GetComponent<HealthController>().Damage(damage);
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    //void Deactivate()
    //{
    //    if (gameObject.activeInHierarchy)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(projectileLifeSpan);
        gameObject.SetActive(false);
    }
}
