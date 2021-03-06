using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Utilities;

namespace Controllers.Core
{
    public class VideoController : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private VideoPlayer videoPlayer;

        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI ellapsedTimeLabel;
        [SerializeField] private TextMeshProUGUI totalTimeLabel;
        [SerializeField] private TextMeshProUGUI currentVideoSpeedLabel;
        [SerializeField] private TextMeshProUGUI currentVolumeLabel;

        [Header("Buttons")]
        [SerializeField] private Button playResumeButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button repeatButton;
        [SerializeField] private Button slowerButton;
        [SerializeField] private Button fasterButton;
        [SerializeField] private Button volumeDownButton;
        [SerializeField] private Button volumeUpButton;
        [SerializeField] private Button muteButton;

        [Header("Sprites")]
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite resumeSprite;
        [SerializeField] private Sprite volumeOnSprite;
        [SerializeField] private Sprite volumeOffSprite;

        [SerializeField] private bool isPlaying = true;

        private AudioSource videoPlayerAudioSource;
        private Image playResumeButtonImage;
        private Image repeatButtonImage;
        private Image muteButtonImage;

        private void Awake()
        {
            videoPlayerAudioSource = videoPlayer.GetComponent<AudioSource>();
            playResumeButtonImage = playResumeButton.GetComponent<Image>();
            repeatButtonImage = repeatButton.GetComponent<Image>();
            muteButtonImage = muteButton.GetComponent<Image>();

            BindEventListeners();
            ellapsedTimeLabel.text = Formatter.GetEllapsedTimeInMinutes(0);
            totalTimeLabel.text = Formatter.GetEllapsedTimeInMinutes((int)videoPlayer.length);
            UpdateCurrentSpeedLabel();
            UpdateVolumeLabel();
        }

        private IEnumerator Start()
        {
            videoPlayer.Play();
            yield return new WaitUntil(() => videoPlayer.isPrepared);
        }

        private void Update()
        {
            UpdateEllapsedTimeLabel();
        }

        /// <summary>
        /// Bind event listeners to events
        /// </summary>
        private void BindEventListeners()
        {
            try
            {
                playResumeButton.onClick.AddListener(() => OnPlayOrResume());
                stopButton.onClick.AddListener(() => OnStop());
                repeatButton.onClick.AddListener(() => OnRepeat());
                slowerButton.onClick.AddListener(() => OnDecelerateVideo());
                fasterButton.onClick.AddListener(() => OnAcelerateVideo());
                volumeDownButton.onClick.AddListener(() => OnVolumeDown());
                volumeUpButton.onClick.AddListener(() => OnVolumeUp());
                muteButton.onClick.AddListener(() => OnMute());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update current ellapsed time to label
        /// </summary>
        private void UpdateEllapsedTimeLabel()
        {
            if (!isPlaying) return;
            ellapsedTimeLabel.text = Formatter.GetEllapsedTimeInMinutes((int)videoPlayer.time);

            if (videoPlayer.time >= videoPlayer.length)
            {
                isPlaying = false;
                playResumeButtonImage.sprite = playSprite;
                return;
            }
        }

        /// <summary>
        /// Play or resume a video
        /// </summary>
        private void OnPlayOrResume()
        {
            isPlaying = !isPlaying;
            if (!isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
            playResumeButtonImage.sprite = (!isPlaying ? playSprite : resumeSprite);
        }

        /// <summary>
        /// Stop the video
        /// </summary>
        private void OnStop()
        {
            isPlaying = false;
            videoPlayer.Stop();
            playResumeButtonImage.sprite = playSprite;
            ellapsedTimeLabel.text = Formatter.GetEllapsedTimeInMinutes(0);
        }

        /// <summary>
        /// Decelerates video's speed
        /// </summary>
        private void OnDecelerateVideo()
        {
            videoPlayer.playbackSpeed -= 0.1f;
            UpdateCurrentSpeedLabel();
        }

        /// <summary>
        /// Acelerates video's speed
        /// </summary>
        private void OnAcelerateVideo()
        {
            videoPlayer.playbackSpeed += 0.1f;
            videoPlayer.playbackSpeed = (videoPlayer.playbackSpeed < 1.5f ? videoPlayer.playbackSpeed : 1.5f);
            UpdateCurrentSpeedLabel();
        }

        /// <summary>
        /// Mute video sound
        /// </summary>
        private void OnMute()
        {
            videoPlayerAudioSource.mute = !videoPlayerAudioSource.mute;
            muteButtonImage.sprite = (videoPlayerAudioSource.mute ? volumeOffSprite : volumeOnSprite);
        }

        /// <summary>
        /// Increase video's volume
        /// </summary>
        private void OnVolumeUp()
        {
            videoPlayerAudioSource.mute = false;
            muteButtonImage.sprite = volumeOnSprite;
            videoPlayerAudioSource.volume += 0.1f;
            UpdateVolumeLabel();
        }

        /// <summary>
        /// Decrease video's volume
        /// </summary>
        private void OnVolumeDown()
        {
            videoPlayerAudioSource.mute = false;
            muteButtonImage.sprite = volumeOnSprite;
            videoPlayerAudioSource.volume -= 0.1f;
            UpdateVolumeLabel();
        }

        /// <summary>
        /// Toggle video loop feature
        /// </summary>
        private void OnRepeat()
        {
            videoPlayer.isLooping = !videoPlayer.isLooping;
            Color current = repeatButtonImage.color;
            current.a = (videoPlayer.isLooping ? 1f : 0.1f);
            repeatButtonImage.color = current;
        }

        /// <summary>
        /// Show current video's playback speed to a label
        /// </summary>
        private void UpdateCurrentSpeedLabel() => currentVideoSpeedLabel.text = string.Concat("x", videoPlayer.playbackSpeed.ToString("F1"));

        /// <summary>
        /// Shows current volume to a label
        /// </summary>
        private void UpdateVolumeLabel() => currentVolumeLabel.text = Mathf.CeilToInt(videoPlayerAudioSource.volume * 10f).ToString();
    }
}