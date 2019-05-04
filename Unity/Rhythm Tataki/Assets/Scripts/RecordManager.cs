
namespace NatCorderU.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using Core;

    public class RecordManager : MonoBehaviour
    {

        // 싱글톤으로 처리
        public static RecordManager instance { get; set; }
        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);
        }

        public AudioSource audioSource;
        public AudioClip audioClip;

        private void Start()
        {
            
        }
        

        public void StartRecording()
        {
            // Create a recording configuration
            const float DownscaleFactor = 2f / 3;
            var configuration = new Configuration((int)(Screen.width * DownscaleFactor), (int)(Screen.height * DownscaleFactor));
            Replay.StartRecording(Camera.main, configuration, OnReplay, audioSource, false);
            Debug.Log("start recording");


        }

        public void StopRecording()
        {
            // Stop recording
            Replay.StopRecording();
            Debug.Log("stop recording");
        }

        void OnReplay(string path)
        {
            Debug.Log("Saved recording to: " + path);

            #if UNITY_IOS || UNITY_ANDROID
            // Playback the video
            // Handheld.PlayFullScreenMovie(path);
            #endif
        }
    }
}



