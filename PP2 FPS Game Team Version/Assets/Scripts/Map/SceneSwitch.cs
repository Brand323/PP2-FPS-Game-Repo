using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string firstPersonSceneName;
    public string mapSceneName;  

    // Call for first person scene
    public void SwitchToFirstPerson()
    {
        SceneManager.LoadScene(firstPersonSceneName);
    }

    // Call for map sc
    public void SwitchToTopDown()
    {
        SceneManager.LoadScene(mapSceneName);
    }
}
