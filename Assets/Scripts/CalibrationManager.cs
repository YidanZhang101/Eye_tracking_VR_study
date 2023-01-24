using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationManager : MonoBehaviour
{
    public Transform stimuli_parent;
    public Transform training_session;
    List<Transform> stimuli_list = new List<Transform>();
    public float timer;

    [Header("Set Remain Time")]
    public float central_cross_remain_time;
    public float peripheral_cross_remain_time;

    [Header("Session Control")]
    public bool StartSession = false;
    public bool EndSession = false;

    bool centerShown = false;

    int stimuli_No = 0;
    bool initialCross = true;

    void Start()
    {
        foreach (Transform stimuli in stimuli_parent)
        {
            if (stimuli.parent == stimuli_parent)
            {
                stimuli_list.Add(stimuli);
            }
        }
    }

    void Update()
    {
        if (StartSession && !EndSession)
        {
            timer += Time.deltaTime;
            float peripheral_time = peripheral_cross_remain_time;
            if (timer >= peripheral_time && !centerShown || initialCross)
            {
                stimuli_list[0].gameObject.SetActive(true);
                centerShown = true;
                for (int i = 1; i < stimuli_list.Count; i++)
                    stimuli_list[i].gameObject.SetActive(false);
                timer = 0;
                stimuli_No++;
                initialCross = false;
            }
            float central_time = central_cross_remain_time;
            if (timer >= central_time && centerShown && stimuli_No < 6)
            {
                stimuli_list[0].gameObject.SetActive(false);
                centerShown = false;
                stimuli_list[stimuli_No].gameObject.SetActive(true);
                timer = 0;
            }
        }
        else if (EndSession)
        {
            training_session.gameObject.SetActive(true);
            stimuli_parent.gameObject.SetActive(false);
            GameObject.Find("Calibration Info").SetActive(false);
        }
    }
}
