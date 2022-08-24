using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public void LoadScene(int i)
    {
        SceneController.LoadScene(i, 1, 2);
    }
 
}
