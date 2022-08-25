using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Over_World_Timer : MonoBehaviour
{
    [SerializeField] int startTimer;
    [SerializeField] int currentTimer;
    [SerializeField] Text countdownText;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Being(startTimer);
    }

    private void Being(int Second)
    {
        currentTimer = Second;
        slider.maxValue = Second;
        slider.value = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(currentTimer>= 0)
        {
            countdownText.text = $"{currentTimer / 60:00}:{currentTimer % 60:00}";
            currentTimer--;
            slider.value = currentTimer;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnd()
    {
		SceneController.LoadScene(2, 1, 2);
    }

    public void incrementTimer(int increment)
    {
        currentTimer += increment;
        if(currentTimer > startTimer)
        {
            startTimer = currentTimer;
            slider.maxValue = currentTimer;
            slider.value = currentTimer;
        }
    }
}
