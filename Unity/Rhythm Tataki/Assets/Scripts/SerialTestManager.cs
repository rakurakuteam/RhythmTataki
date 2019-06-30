using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class SerialTestManager : MonoBehaviour
{
    // 시리얼 통신
    public SerialPort serial;
    private string portName = "COM4";   // 포트이름
    private int baudRate = 115200;        // 통신속도
    private Stopwatch sw = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        // 시리얼 포트 셋팅
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 1;
        serial.Open();
    }

    private void Update()
    {
        if (serial.IsOpen)
        {
            sw.Start();
            try
            {
                ReadSerialValue();
            }
            catch (TimeoutException e)
            {
                // Debug.Log("serial Error: " + e.ToString());
            }
        }
    }

    // 아두이노에서 보내느 값을 읽고 처리한다.
    public void ReadSerialValue()
    {
        string input = serial.ReadChar().ToString();

        if (input.Equals("1"))
        {
         
            Debug.Log("1");
            sw.Stop();
            Debug.Log("sw: " + sw.Elapsed.Milliseconds.ToString() + "ms");
            sw.Reset();
        }

        if (input.Equals("2"))
        {
        
            Debug.Log("2");
            sw.Stop();
            Debug.Log("sw: " + sw.Elapsed.Milliseconds.ToString() + "ms");
            sw.Reset();
        }

        
    }

}
