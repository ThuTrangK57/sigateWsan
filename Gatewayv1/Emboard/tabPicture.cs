using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Emboard
{
    public partial class Emboard : Form
    {
        private void btTakephoto_Click(object sender, System.EventArgs e)//lenh lay hinh anh
        {
            try
            {
                Database myDatabase = new Database();
                int now = DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second;
                InformationNode.timeDapUng.Remove(sensor.Mac);
                InformationNode.timeDapUng.Add(sensor.Mac, now);
                sensor.Mac = cbnodeImg.Text.Substring(7, 2);
                if (sensor.Mac[0] == '0')
                {
                    sensor.Ip = myDatabase.getNetworkIpSensor(sensor.Mac);
                }
                else
                {
                    sensor.Ip = myDatabase.getNetworkIpSensorBC(sensor.Mac);
                }
                sensor.Command = sensor.Ip + "444$";    //lenh chup anh o sensor.Ip
                byte[] commandbyte = comPort.ConvertTobyte(sensor.Command);
                comPort.DisplayData("(" + comPort.showTime() + "): Gui lenh chup anh!! (" + sensor.Mac + "):\r\n Ma lenh : " + commandbyte.ToString(), tbShow2);
                comPort.writeByteData(commandbyte);
            }
            catch
            {
                MessageBox.Show("Ban chua nhap thong tin node", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }                
    }
}

