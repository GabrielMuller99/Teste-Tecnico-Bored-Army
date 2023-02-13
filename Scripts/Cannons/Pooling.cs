using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject projectilesToInstantiate;
    public int beginInstantiated;

    public List<GameObject> projectilesList;

    public bool canIncrease;

    void Start()
    {
        projectilesList = new List<GameObject>();

        for (int i = 0; i < beginInstantiated; i++)
        {
            projectilesList.Add(Instantiate(projectilesToInstantiate));
            projectilesList[i].SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < projectilesList.Count; i++)
        {
            if (!projectilesList[i].activeInHierarchy)
            {
                return projectilesList[i];
            }
        }

        if (!canIncrease)
        {
            return null;
        }

        projectilesList.Add(Instantiate(projectilesToInstantiate));
        return projectilesList[projectilesList.Count-1];
    }
}
