using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoPlayerController : MonoBehaviour, IPointerClickHandler
{
    public RawImage rawImage;
    public RenderTexture renderTexture; 
    public VideoPlayer videoPlayer;
    public bool triggerable = false; // if not set then can't be replayed by clicking 
    private Button playBtn;

    private bool isPlaying = false;
    private Outline outline; 
    void Start()
    {
        // videoPlayer = GetComponent<VideoPlayer>();
        Debug.Log("Video player?:"+videoPlayer);
        // Play the video when the GameObject awakens
        // Subscribe to the video's loopPointReached event
        videoPlayer.loopPointReached += VideoFinished;
        //PlayVideo();
        Transform t = transform.Find("PlayBtn");
        if(playBtn!= null)
        {

            playBtn = t.GetComponent<Button>();
           
            playBtn.onClick.AddListener(ReplayVideo);
        }
        Debug.Log("Play Button:" + playBtn);

        videoPlayer.Play();
        videoPlayer.prepareCompleted += OnFirstFrameReady;

        outline = GetComponent<Outline>();
        Debug.Log("Outline:"+outline.effectDistance); 
    }

    void OnFirstFrameReady(VideoPlayer vp)
    {
        // Pause the video after capturing the first frame
        videoPlayer.Pause();

        // Reset to the beginning of the video
        videoPlayer.frame = 0;

        // Unsubscribe from the event to prevent it from being called again
        videoPlayer.prepareCompleted -= OnFirstFrameReady;
    }

    public void PlayVideo()
    {
        if (videoPlayer != null)
        {
            // Set the RawImage texture to the VideoPlayer's target texture
            Debug.Log("Set the RawImage texture to the VideoPlayer's target texture");
            rawImage.texture = videoPlayer.targetTexture;

            // Play the video
            videoPlayer.Play();
            isPlaying = true;


        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Video playing?:"+ isPlaying); 
        if (triggerable && !isPlaying)
        {
            // Replay the video if it's not playing

            ReplayVideo();
        }
    }
    public void ReplayVideo()
    {
        Debug.Log("Now replaying video"); 
        if (videoPlayer != null)
        {
            Debug.Log(videoPlayer);
            //BtnAnimation("FadeOut")
            if(playBtn!=null)playBtn.gameObject.SetActive(false); 
            // Stop and replay the video from the beginning
            videoPlayer.Stop();
            videoPlayer.enabled = true;
            videoPlayer.frame = 0;
            videoPlayer.Play();
            isPlaying = true;
            if (outline != null)
            {
                outline.effectDistance = new Vector2(10, 10); 
            }
        }
    }

    public void VideoFinished(VideoPlayer vp)
    {
        isPlaying = false;
        // BtnAnimation("FadeIn");
        if (playBtn != null) playBtn.gameObject.SetActive(true);
        if (outline != null)
        {
            outline.effectDistance = new Vector2(1, 1);
        }
    }
}
