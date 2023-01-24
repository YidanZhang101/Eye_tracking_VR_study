using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStimuli : MonoBehaviour
{
    public Transform stimuli_parent;
    List<Transform> stimuli_list = new List<Transform>();
    public float timer;
    public float[] central_cross_remain_time;
    public float[] peripheral_cross_remain_time;

    [Header("Trial Number for Half Session")]
    public int trial_number = 24;

    [Header("Counter (for monitoring only)")]
    public int left_5 = 0;
    public int right_5 = 0;
    public int left_10 = 0;
    public int right_10 = 0;

    int trial_counter = 0;

    [Header("Session Control")]
    public bool StartSession = false;
    public bool SecondHalfSession = false;

    bool centerShown = false;
    bool secondHalfStarted = false;

    Dictionary<int, int> targetsCount = new Dictionary<int, int>();

    void Start()
    {    
        foreach (Transform stimuli in stimuli_parent)
        {
            if (stimuli.parent == stimuli_parent)
            {
                stimuli_list.Add(stimuli);
            }
        }
        targetsCount.Add(1, left_5);
        targetsCount.Add(2, right_5);
        targetsCount.Add(3, left_10);
        targetsCount.Add(4, right_10);
    }

    void Update()
    {
        //if (MoveStimuli.TraningSessionCompleted)
        //{
        //    stimuli_parent.gameObject.SetActive(true);
        //    GameObject.Find("ListOfStimuli_training").SetActive(false);
        //}
        if (StartSession)
        {
            if (SecondHalfSession && !secondHalfStarted)
            {
                trial_counter = 0;
                timer = 0;
                secondHalfStarted = true;
            }

            timer += Time.deltaTime;
            float peripheral_time = peripheral_cross_remain_time[RandomisePeripheralCrossIndex()];
            if (timer >= peripheral_time && !centerShown)
            {
                Debug.Log("peripheral_cross_remain_time[RandomisePeripheralCrossIndex()]: " + peripheral_time);
                stimuli_list[0].gameObject.SetActive(true);
                centerShown = true;
                for (int i = 1; i < stimuli_list.Count; i++)
                    stimuli_list[i].gameObject.SetActive(false);
                timer = 0;
            }
            float central_time = central_cross_remain_time[RandomiseCentralCrossIndex()];
            if (timer >= central_time && centerShown && trial_counter < trial_number)
            {
                Debug.Log("central_cross_remain_time[RandomiseCentralCrossIndex()]: " + central_time);
                stimuli_list[0].gameObject.SetActive(false);
                centerShown = false;
                stimuli_list[RandomiseTargetIndex()].gameObject.SetActive(true);
                timer = 0;

                trial_counter++;
            }
        }       
    }

    public int RandomiseCentralCrossIndex()
    {
        int central_index = RandomNumber(0, central_cross_remain_time.Length);
        return central_index;
    }

    public int RandomisePeripheralCrossIndex()
    {
        int peripheral_index = RandomNumber(0, peripheral_cross_remain_time.Length);
        return peripheral_index;
    }

    public int RandomiseTargetIndex()
    {
        int index = RandomNumber(1, 5);
        while(targetsCount[index] >= trial_number/2)
            index = RandomNumber(1, 5);
        AddCounter(index);
        return index;
    }

    public int RandomNumber(int min, int max)
    {
        return (int)UnityEngine.Random.Range(min, max);
    }

    private void AddCounter(int index)
    {
        if (index == 1)
        {
            left_5++;
            targetsCount[index] = left_5;
        }          
        if (index == 2)
        {
            right_5++;
            targetsCount[index] = right_5;
        }       
        if (index == 3)
        {
            left_10++;
            targetsCount[index] = left_10;
        }       
        if (index == 4)
        {
            right_10++;
            targetsCount[index] = right_10;
        }       
    }
}
