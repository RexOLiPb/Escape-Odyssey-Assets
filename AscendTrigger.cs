using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class AscendTrigger : PickUpItem
    {
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
            //Debug.Log("Picked up S1 trigger");
            //SceneManager.LoadScene(targetScene, LoadSceneMode.Single);
            PlayerController player = FindObjectOfType<PlayerController>();
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            //rb.velocity = new Vector2(0, 50);
            Debug.Log("Ascending!");
            //StartCoroutine(SwitchSceneAfterDelay());
            string targetScene = "SceneV2";
            SceneManager.LoadScene(targetScene,LoadSceneMode.Single);
            /////////////////////////////////////////////////
        }
        IEnumerator SwitchSceneAfterDelay()
        {
            string targetScene = "EndScene";
            yield return new WaitForSeconds(3);
            Debug.Log("Switch!");
            SceneManager.LoadScene(targetScene); 
        }
    }
}
