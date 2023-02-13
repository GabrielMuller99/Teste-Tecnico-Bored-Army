using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsParameters : MonoBehaviour
{
    public static SettingsParameters instance;

    public float timerInput;
    public float respawnInput;

    [SerializeField] GameObject warningTimer;
    [SerializeField] GameObject warningRespawn;

    public Values timers;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        instance = this;
    }

    public void SetTimer(string value)
    {
        var aux = float.Parse(value);

        if (aux >= 60 && aux <= 180)
        {
            timerInput = float.Parse(value);
            timers.timerValue = timerInput;
            warningTimer.SetActive(false);
        }
        else
        {
            warningTimer.SetActive(true);
        }
    }

    public void SetRespawnTimer(string value)
    {
        var aux = float.Parse(value);

        if (aux > 0 && aux < timerInput)
        {
            respawnInput = float.Parse(value);
            timers.respawnValue = respawnInput;
            warningRespawn.SetActive(false);
        }
        else
        {
            warningRespawn.SetActive(true);
        }
    }
}
