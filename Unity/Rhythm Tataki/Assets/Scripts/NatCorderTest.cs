
namespace NatCorderU.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using Core;

    public class NatCorderTest : MonoBehaviour
    {
        [Header("Recording")]
        public bool recordMicrophoneAudio;
        public AudioSource audioSource;
        public AudioClip audioClip;

        // 추가
        public Text timeText;
        public GameObject startRecButton, stopRecButton, recAnimation;
        private bool recoding = false; // 녹화중인지 아닌지?

        private const float MAX_RECORDING_TIME = 30f;
        private float time = 0.0f;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioClip = Resources.Load<AudioClip>("Beats/솜사탕");
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        private void Update()
        {
            if (recoding)
            {
                time += Time.deltaTime;
                timeText.text = string.Format("00 : {0}", time.ToString("00"));

                if (time >= MAX_RECORDING_TIME)
                {
                    StopRecording();   
                }
            }
        }

        // 녹화가 끝나면 시간과 현재 녹화중이 아니라는 것을 알려주기 위한 초기화 메서드
        private void Reset()
        {
            time = 0.0f;
            recoding = false;
        }

        // 녹화를 시작했을 때, 녹화가 되었다는 것으로 변경, 녹화 버튼을 끄고 녹화중에 필요한 오브젝트들을 킨다.
        private void StartRec()
        {
            recoding = true;
            stopRecButton.SetActive(true);
            startRecButton.SetActive(false);
            // recAnimation.SetActive(true);
            timeText.gameObject.SetActive(true);
        }

        // 녹화가 종료 되었을 때, 녹화에 필요한 값들을 초기화 하고, 녹화 버튼을 키고 녹화중이 아닐 때 필요없는 오브젝트를 끈다.
        private void StopRec()
        {
            Reset();
            stopRecButton.SetActive(false);
            startRecButton.SetActive(true);
            // recAnimation.SetActive(false);
            timeText.gameObject.SetActive(false);
        }
        
        public void StartRecording()
        {
            StartRec();

            // Create a recording configuration
            const float DownscaleFactor = 2f / 3;
            var configuration = new Configuration((int)(Screen.width * DownscaleFactor), (int)(Screen.height * DownscaleFactor));

            // Start recording with microphone audio
            if (recordMicrophoneAudio)
            {
                // Start the microphone
                // audioSource.clip = Extensions.Microphone.StartRecording();
                audioSource.clip = Microphone.Start(null, true, 60, 48000);
                // audioSource.loop = true;
                audioSource.Play();
                // Start recording with microphone audio
                Replay.StartRecording(Camera.main, configuration, OnReplay, audioSource, false);
                Debug.Log("microphone function");
            }

            // Start recording without microphone audio
            else
            {
                Replay.StartRecording(Camera.main, configuration, OnReplay, audioSource, false);
                Debug.Log("else function");
            }
                
        }

        public void StopRecording()
        {
            StopRec();

            // Stop playing mic audio
            if (recordMicrophoneAudio)
            {
                audioSource.Stop();
                Extensions.Microphone.StopRecording();
            }
            // Stop recording
            Replay.StopRecording();
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



