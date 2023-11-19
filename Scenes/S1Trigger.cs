using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Platformer
{
    public class S1Trigger : PickUpItem
    {
        private string targetScene = "SampleScene";
        // Start is called before the first frame update
        void Start()
        {
            OnPickedUP += OnPickedEvent;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnPickedEvent()
        {
            /////////////////////////////////////////////////Here your logic
            Debug.Log("Picked up S1 trigger");
            SceneManager.LoadScene(targetScene,LoadSceneMode.Single);
            /////////////////////////////////////////////////
        }
    }
}
