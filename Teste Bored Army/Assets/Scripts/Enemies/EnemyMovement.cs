using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavigationList navPoints;
    [SerializeField] List<Transform> navPointsList;

    [SerializeField] Transform currentTarget;
    public int index;
    public float distanceBetweenTarget;

    public float enemySpeed;
    public float enemyRotationSpeed;
    public float enemyRotationModifier;

    bool beginMoving;

    void OnEnable()
    {
        beginMoving = true;
        navPointsList = new List<Transform>();
        DeterminePath();
        index = 0;
        currentTarget = navPointsList[index];
    }

    public virtual void Update()
    {
        Vector2 destination = currentTarget.position - transform.position;
        destination.Normalize();

        if (Vector2.Distance(currentTarget.position, transform.position) <= distanceBetweenTarget)
        {
            index++;

            if (index >= navPointsList.Count)
            {
                index = 0;
            }

            currentTarget = navPointsList[index];
            beginMoving = false;
            StartCoroutine(WaitForBoatToTurn());
        }

        if (beginMoving)
        {
            transform.Translate(enemySpeed * Time.deltaTime * destination, Space.World);
        }

        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg - enemyRotationModifier;
        Quaternion aux = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, aux, Time.deltaTime * enemyRotationSpeed);
    }

    void DeterminePath()
    {
        for (int i = 0; i < 3; i++)
        {
            int rng = Random.Range(0, navPoints.navigation.Count - 1);

            if (i <= 0)
            {
                navPointsList.Add(navPoints.navigation[rng]);
            }
            else
            {
                navPointsList.Add(navPoints.navigation[rng]);

                while (navPointsList[i] == navPointsList[i - 1])
                {
                    int rngAux = Random.Range(0, navPoints.navigation.Count - 1);
                    navPointsList[i] = navPoints.navigation[rngAux];
                }
            }
        }
    }

    IEnumerator WaitForBoatToTurn()
    {
        yield return new WaitForSeconds(1f);
        beginMoving = true;
    }
}
