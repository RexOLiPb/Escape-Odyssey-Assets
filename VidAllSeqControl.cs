using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
namespace Platformer {
    public class VidAllSeqControl : MonoBehaviour, IPointerClickHandler
    {
        private int curVidIdx = 0;
        private AnchorPanels[] controlAnchors;
        private List<VideoPlayerController> allVideoControllers = new List<VideoPlayerController>();
        private Dictionary<AnchorPanels, int> mp = new Dictionary<AnchorPanels, int>();
        private int[] correctOrder = { 0, 1, 2, 3 };
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Hello from video player seq!");
            Transform topContainer = transform.parent.transform.parent.transform.parent;
            Debug.Log("Top Container:" + topContainer.name);
            AnchorPanels[] anchors = topContainer.GetComponentsInChildren<AnchorPanels>(true);
            //VideoPlayerController[] allVideoControllers;
            //List<VideoPlayerController> allVideoControllers = new List<VideoPlayerController>();
            for (int i = 0; i < anchors.Length; i++)
            {
                VideoPlayerController[] vidControllers = anchors[i].GetComponentsInChildren<VideoPlayerController>();
                allVideoControllers.AddRange(vidControllers);
            }
            //allVideoControllers.AddRange = GetComponentInChildren<AnchorPanels>(true)?.GetComponentsInChildren<VideoPlayerController>();
            Debug.Log("Found video controllers:" + allVideoControllers.Count());
            for (int i = 0; i < anchors.Length; i++)
            {
                mp[anchors[i]] = i;
            }
            foreach (KeyValuePair<AnchorPanels, int> entry in mp)
            {
                Debug.Log(entry.Key.name + entry.Value);
            }
            controlAnchors = anchors;
            // Debug.Log(allVideoControllers[0]); 
            //PlayNextVideo();
        }
        bool checkOrder()
        {
            //Debug.Log("Order:"); 
            //for(int i = 1; i < allVideoControllers.Count()-1; i++)
            //{
            //    int correct = correctOrder[i - 1]; 
            //    VideoPlayerController ctrl = allVideoControllers[i]; 
            //    AnchorPanels anchor = ctrl.transform.parent?.GetComponentInChildren<AnchorPanels>();
            //    if (anchor != null && mp.ContainsKey(anchor))
            //    {
            //        int actual = mp[anchor]; 
            //        Debug.Log(mp[anchor]+",");
            //        if (actual != correct) return false; 
            //    }
            //}
            if (curVidIdx < 0 || curVidIdx >= allVideoControllers.Count()) return false;
            VideoPlayerController ctrl = allVideoControllers[curVidIdx];
            AnchorPanels anchor = ctrl.transform.parent?.GetComponentInChildren<AnchorPanels>();
            int correct = correctOrder[curVidIdx];
            if (anchor != null && mp.ContainsKey(anchor))
            {
                int actual = mp[anchor];
                Debug.Log(mp[anchor] + ",");
                if (actual != correct)
                {
                    Debug.Log("Actual:" + actual + " Correct:" + correct);
                    return false;
                }
            }
            return true;

        }
        void PlayNextVideo()
        {
            int totalVidLen = allVideoControllers.Count();
            if (curVidIdx < totalVidLen)
            {
                pauseAllVids();
                VideoPlayerController vidCtrl = allVideoControllers[curVidIdx];

                vidCtrl.ReplayVideo();
                vidCtrl.videoPlayer.loopPointReached += OnVideoFinished;
                bool order = checkOrder();
                if (!order)
                {
                    vidCtrl.videoPlayer.loopPointReached -= OnVideoFinished;
                    gameObject.SetActive(true);
                    return;
                }
                curVidIdx++;
            }
            else
            {
                Debug.Log("All videos played");
                gameObject.SetActive(true);
                SceneManager.sceneLoaded += SceneLoaded; 
                SceneManager.LoadScene("GameScene");

            }
        }
        void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            S1Target s1_target = FindObjectOfType<S1Target>();
            player.transform.position = s1_target.transform.position; 
        }
        void pauseAllVids()
        {
            foreach (VideoPlayerController vidCtrl in allVideoControllers)
            {
                vidCtrl.videoPlayer.Pause();
            }
        }
        void OnVideoFinished(VideoPlayer vp)
        {
            // Unsubscribe from the loopPointReached event
            vp.loopPointReached -= OnVideoFinished;

            // Play the next video in the sequence
            PlayNextVideo();
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // throw new System.NotImplementedException();
            curVidIdx = 0;
            gameObject.SetActive(false);
            PlayNextVideo();
        }
    }
}