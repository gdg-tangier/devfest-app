using UnityEngine;
using EasyAR;

public class VideoPlay : ImageTargetBehaviour
{
    private bool loaded;
    private bool found;
    private System.EventHandler videoReayEvent;
    private VideoPlayerBaseBehaviour videoPlayer;
    public GameObject PlayerLoc;

    protected override void Awake()
    {
        base.Awake();
        TargetFound += OnTargetFound;
        TargetLost += OnTargetLost;
        TargetLoad += OnTargetLoad;
        TargetUnload += OnTargetUnload;
    }

    protected override void Start()
    {
        videoReayEvent = OnVideoReady;
        base.Start();
        LoadVideo("Android.mp4");
    }

    public void SwitchVideo(string video)
    {
        UnLoadVideo();
        LoadVideo(video);
    }

    public void LoadVideo(string video)
    {
        videoPlayer = PlayerLoc.GetComponent<VideoPlayerBehaviour>();
        if (videoPlayer)
        {
            videoPlayer.Storage = StorageType.Assets;
            videoPlayer.Path = video;
            videoPlayer.EnableAutoPlay = true;
            videoPlayer.EnableLoop = true;
            videoPlayer.Type = VideoPlayerBehaviour.VideoType.Normal;
            videoPlayer.VideoReadyEvent += videoReayEvent;
            videoPlayer.Open();
        }
    }

    public void UnLoadVideo()
    {
        if (!videoPlayer)
            return;
        videoPlayer.VideoReadyEvent -= videoReayEvent;
        videoPlayer.Close();
        loaded = false;
    }

    void OnVideoReady(object sender, System.EventArgs e)
    {
        Debug.Log("Load video success");
        VideoPlayerBaseBehaviour player = sender as VideoPlayerBaseBehaviour;
        loaded = true;
        if (player && found)
            player.Play();
    }

    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
        found = true;
        if (videoPlayer && loaded)
            videoPlayer.Play();
        Debug.Log("Found: " + Target.Id);
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
        found = false;
        if (videoPlayer && loaded)
            videoPlayer.Pause();
        Debug.Log("Lost: " + Target.Id);
    }

    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
        Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }

    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
        Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }
}
