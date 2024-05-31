using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public int NumberOfSeconds;
    public Action TimerDone;
    public TMPro.TextMeshProUGUI DisplayText;

    float currentNumberOfSeconds;
    bool countingDown = false;
    // Start is called before the first frame update
    void Start()
    {
        countingDown = false;
    }
    public void StartCountDown()
    {
        gameObject.SetActive(true);
        countingDown = true;
        currentNumberOfSeconds = 0;
    }
    public void ConfirmFinished()
    {
        countingDown = false;
        gameObject.SetActive(false);
        TimerDone = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (!countingDown) return;
        currentNumberOfSeconds += Time.deltaTime;
        DisplayText.text = "" + (NumberOfSeconds - (int)(currentNumberOfSeconds));
        if (currentNumberOfSeconds > NumberOfSeconds)
        {
            TimerDone?.Invoke();
        }

    }
}
