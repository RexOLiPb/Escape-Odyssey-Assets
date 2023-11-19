using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IPointerClickHandler
{
    private string targetSceneName = "SampleScene";

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Jumping to scene " + targetSceneName); 
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
