using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlaySeq : MonoBehaviour
{
    private int curVidIdx = 0;
    private List<VideoPlayerController> allVideoControllers = new List<VideoPlayerController>(); 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello from video player seq!");
        AnchorPanels[] anchors = GetComponentsInChildren<AnchorPanels>(true);
        //VideoPlayerController[] allVideoControllers;
        //List<VideoPlayerController> allVideoControllers = new List<VideoPlayerController>();
        for(int i = 0;i< anchors.Length; i++)
        {
            VideoPlayerController[] vidControllers = anchors[i].GetComponentsInChildren<VideoPlayerController>();
            allVideoControllers.AddRange(vidControllers); 
        }
        //allVideoControllers.AddRange = GetComponentInChildren<AnchorPanels>(true)?.GetComponentsInChildren<VideoPlayerController>();
        Debug.Log("Found video controllers:" + allVideoControllers.Count());
        // Debug.Log(allVideoControllers[0]); 
        PlayNextVideo(); 
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
            curVidIdx++;
        }
        else
        {
            Debug.Log("All videos played");
        }
    }
    void pauseAllVids()
    {
        foreach(VideoPlayerController vidCtrl in allVideoControllers)
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
}
