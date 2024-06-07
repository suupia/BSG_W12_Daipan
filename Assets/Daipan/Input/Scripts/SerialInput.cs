using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

namespace Daipan.InputSerial.Scripts
{
    public class SerialInput : MonoBehaviour
    {

        [SerializeField] private string portName = "COM?";
        [SerializeField] private int baurate = 115200;

        private SerialPort serial;
        private bool isLoop = true;
        private Thread thread;


        public int number = 0;
        void Start()
        {
            this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);
            serial.DtrEnable = true;
            serial.RtsEnable = true;

            //テスト

            for(int portNum = 0; portNum < 11; portNum++)
            {
                serial = new SerialPort(portName + portNum, baurate, Parity.None, 8, StopBits.One);
                serial.DtrEnable = true;
                serial.RtsEnable = true;
                try
                {
                    serial.Open();
                }
                catch(Exception e)
                {
                    Debug.LogWarning(portName + portNum + "ポートが開けませんでした。設定している値が間違っている場合があります" +
                         e.Message);
                    continue;
                }
                portNum = 1000;
            }
            //try
            //{
            //    this.serial.Open();

            //}
            //catch (Exception e)
            //{
            //    Debug.LogWarning("ポートが開けませんでした。設定している値が間違っている場合があります" +
            //              e.Message);
            //}

            try
            {
                thread = new Thread(ReadData);
                thread.Start();
                //別スレッドで実行  
                //Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
            }
            catch
            {
                Debug.Log("なんかミスってる");
            }
        }

        //データ受信時に呼ばれる
        public void ReadData()
        {
            while (this.isLoop)
            {
                //ReadLineで読み込む
                string message = this.serial.ReadLine();
                Debug.Log(message);
                number = int.Parse(message);
            }
        }

        void OnDestroy()
        {
            this.isLoop = false;
            this.serial.Close();
        }
    }
}