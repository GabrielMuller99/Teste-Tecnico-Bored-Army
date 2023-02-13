using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrigger : MonoBehaviour
{
    [SerializeField] Pooling projectilePooling;
    [SerializeField] Pooling smallExplosionPooling;

    [SerializeField] GameObject firePosition;
    [SerializeField] GameObject firePositionSide1;
    [SerializeField] GameObject firePositionSide2;
    [SerializeField] GameObject firePositionSide3;

    List<GameObject> sidePositionList;

    public float fireRate;
    float timer;

    void Start()
    {
        sidePositionList = new List<GameObject>
        {
            firePositionSide1,
            firePositionSide2,
            firePositionSide3
        };
        timer = 0;
    }

    void Update()
    {
        if (timer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Invoke("Shoot", 0);
                timer = fireRate;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Invoke("SideShoot", 0);
                timer = fireRate;
            }
        }

        timer -= Time.deltaTime;
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

    void SideShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newShot = projectilePooling.GetObject();
            GameObject newExplosion = smallExplosionPooling.GetObject();

            if (newShot != null)
            {
                newShot.transform.position = sidePositionList[i].transform.position;
                newShot.transform.rotation = sidePositionList[i].transform.rotation;
                newShot.SetActive(true);

                newExplosion.transform.position = sidePositionList[i].transform.position;
                newExplosion.transform.rotation = sidePositionList[i].transform.rotation;
                newExplosion.SetActive(true);

                newExplosion.transform.SetParent(this.transform);
            }
        }
    }
}
