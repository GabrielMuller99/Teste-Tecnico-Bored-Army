using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : EnemyMovement
{
    [Header("Chaser Controller")]
    public float speedBoost;
    public int damage;
    Transform target; 
    [SerializeField] Pooling explosionPooling;

    [Header("OverlapCircle Parameters")]
    [SerializeField] Transform detectorOrigin;

    public float detectorSize;

    public LayerMask detectorLayerMask;

    public Color gizmoColor;
    public bool showGizmos = true;

    public override void Update()
    {
        if (target == null)
        {
            PlayerDetection();
            base.Update();
        }
        else
        {
            Vector2 destination = target.position - transform.position;
            destination.Normalize();

            transform.Translate((enemySpeed + speedBoost) * Time.deltaTime * destination, Space.World);

            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg - enemyRotationModifier;
            Quaternion aux = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, aux, Time.deltaTime * enemyRotationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthController>().Damage(damage);

            GameObject newExplosion = explosionPooling.GetObject();
            newExplosion.transform.position = transform.position;
            newExplosion.transform.rotation = transform.rotation;
            newExplosion.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    void PlayerDetection()
    {
        Collider2D collider = Physics2D.OverlapCircle(detectorOrigin.position,
            detectorSize, detectorLayerMask);

        if (collider != null)
        {
            target = collider.gameObject.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(detectorOrigin.position, detectorSize);
        }
    }
}
