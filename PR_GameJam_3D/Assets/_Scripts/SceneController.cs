using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class SceneController : MonoBehaviour 
{

    public Image fader;

    private static SceneController instance;


     void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            fader.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
            fader.gameObject.SetActive(false); 

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(int index, float duration = 1, float waitTime = 0)
    {
       instance.StartCoroutine(instance.FaderScene(index, duration, waitTime));
    }

    private IEnumerator FaderScene(int index, float duration, float waitTime)
    {
        Debug.Log("in coroutine");
        fader.gameObject.SetActive(true);


        for(float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0,1,t));
            yield return null;
                       
        }
     
        AsyncOperation ao = SceneManager.LoadSceneAsync(index);     
        
        while(!ao.isDone)
            yield return null;

        yield return new WaitForSeconds(waitTime);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
            yield return null;

        }
        fader.gameObject.SetActive(false);
    }
}
