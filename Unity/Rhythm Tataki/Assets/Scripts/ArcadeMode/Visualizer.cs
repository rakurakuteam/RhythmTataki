using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class Visualizer : MonoBehaviour
    {

        public GameObject bar;//막대 원본

        public Transform barsParent;//막대 부모

        private RectTransform[] bars = new RectTransform[64];//막대들

        private AudioSource audioSur;//오디오 소스

        public float[] samples = new float[64]; //샘플//샘플수는 64가 최소 2의 거듭제곱으로 해야함

        private void OnEnable()
        {
            for (int i = 0; i < bars.Length; i++)//막대들 만들기
            {
                bars[i] = (Instantiate(bar) as GameObject).GetComponent<RectTransform>();//막대생성
                bars[i].parent = barsParent;//부모설정
                bars[i].localPosition = new Vector2(i * 10, 0);//포지션설정

                //색깔 랜덤
                bars[i].GetComponent<Image>().color =
                    new Vector4(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);

                bars[i].sizeDelta = new Vector2(5, 1);//사이즈 초기화
            }

            audioSur = GetComponent<AudioSource>();//오디오 소스 가지고오기

            audioSur.Play();//플레이
        }

        private void Update()
        {
            //스펙트럼 가지고오는 함수
            audioSur.GetSpectrumData(samples, 0, FFTWindow.Rectangular);

            for (int i = 0; i < 64; i++)//크기 조절
            {
                bars[i].sizeDelta = new Vector2(5, samples[i] * 700);
            }
        }
    }

