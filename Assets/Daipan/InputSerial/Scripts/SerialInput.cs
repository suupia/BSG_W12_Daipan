using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

namespace Daipan.InputSerial.Scripts
{
    public class SerialInput
    {

        private string portName = "COM";
        private int baurate = 115200;
        private int MAX_PORT_NUM = 10;

        private SerialPort serial;
        private bool isLoop = true;
        private Thread thread;


        public int number = 0;
        public bool isSerial = false;
        public SerialInput()
        {
            this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);
            serial.DtrEnable = true;
            serial.RtsEnable = true;

            for(int portNum = 0; portNum <= MAX_PORT_NUM; portNum++)
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
                    serial = null;
                    continue;
                }
                portNum = MAX_PORT_NUM + 10;
            }
            // ポートがない場合
            if (serial == null)
            {
                Debug.LogWarning("ポートがありませんでした。");
                return;
            }


            try
            {
                thread = new Thread(ReadData);
                thread.Start();
                //別スレッドで実行  
                //Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
            }
            catch
            {
                Debug.LogWarning("なんかミスってる");
                return;
            }
            isSerial = true;
        }

        //データ受信時に呼ばれる
        public void ReadData()
        {
            while (this.isLoop)
            {
                //ReadLineで読み込む
                string message = this.serial.ReadLine();
                //Debug.Log(message);
                number = int.Parse(message);
            }
        }

        ~SerialInput()
        {
            this.isLoop = false;
            this.serial.Close();
        }
    }
}