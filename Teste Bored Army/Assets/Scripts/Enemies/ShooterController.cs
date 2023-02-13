using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : EnemyMovement
{
    [Header("Shooter Controller")]
    [SerializeField] Pooling projectilePooling;
    [SerializeField] Pooling smallExplosionPooling;

    [SerializeField] GameObject firePosition;

    public float fireRate;
    public float rotationSpeedBoost;
    float timer;

    Transform target;

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

            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg - enemyRotationModifier;
            Quaternion aux = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, aux, 
                Time.deltaTime * (enemyRotationSpeed + rotationSpeedBoost));

            if (timer <= 0)
            {
                Invoke("Shoot", 0);
                timer = fireRate;
            }

            timer -= Time.deltaTime;

            PlayerDetection();
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
            timer = fireRate - 1f;
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

    void Shoot()
    {
        GameObject newShot = projectilePooling.GetObject();
        GameObject newExplosion = smallExplosionPooling.GetObject();

        if (newShot != null)
        {
            newShot.transform.position = firePosition.transform.position;
            newShot.transform.rotation = firePosition.transform.rotation;
            newShot.SetActive(true);

            newExplosion.transform.position = firePosition.transform.position;
            newExplosion.transform.rotation = firePosition.transform.rotation;
            newExplosion.SetActive(true);

            newExplosion.transform.SetParent(this.transform);
        }
    }
}
