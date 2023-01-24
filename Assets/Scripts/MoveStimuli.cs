using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveStimuli : MonoBehaviour
{

    public Transform stimuli_parent_training;
    public Transform real_session;
    public Transform study_info;
    public float timer;
    public float cross_remain_time;

    [Header("Traning Session Control")]
    public bool StartSession = false;
    public bool RestartSession = false;
    public bool SessionRestarted = false;
    public bool TraningSessionCompleted = false;

    List<Transform> stimuli_list = new List<Transform>();
    int stimuli_No = 0;

    void Start()
    {
        foreach (Transform stimuli in stimuli_parent_training)
        {
            if (stimuli.parent == stimuli_parent_training)
            {
                stimuli_list.Add(stimuli);
            }
        }
    }

    void Update()
    {
        if (StartSession)
        {
            if (RestartSession && !SessionRestarted)
            {
                stimuli_No = 0;
                timer = 0;
                SessionRestarted = true;
            }

            timer += Time.deltaTime;
            if (timer >= cross_remain_time & stimuli_No < 6)
            {
                foreach (Transform stimuli in stimuli_list)
                    stimuli.gameObject.SetActive(false);

                stimuli_list[stimuli_No].gameObject.SetActive(true);

                stimuli_No++;
                timer = 0;
            }
        }

        if (TraningSessionCompleted)
        {
            //stimuli_parent_training.gameObject.SetActive(false);
            //SceneManager.LoadScene("Eye_Tracking", LoadSceneMode.Additive);
            real_session.gameObject.SetActive(true);
            stimuli_parent_training.gameObject.SetActive(false);
            //Transform study_info = GameObject.Find("Study Info").transform;
            study_info.gameObject.SetActive(true);

        }           
    }

}


