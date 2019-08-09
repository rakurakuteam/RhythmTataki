using UnityEngine;
using System.IO.Ports;
using System;

public class SerialManager : MonoBehaviour
{
    // 시리얼 통신
    public SerialPort serial;
    private string portName = "COM4";   // 포트이름
    private int baudRate = 9600;        // 통신속도

    // 싱글톤
    public static SerialManager instance { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 시리얼 포트 셋팅
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 1;
        serial.Open();
    }
}
