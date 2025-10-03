using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System;
using System.Windows.Forms;

namespace MissionPlanner.plugins
{
    public class example_gpsraw_console : Plugin.Plugin
    {
        public override string Name { get; } = "mavlink-console";
        public override string Version { get; } = "1.0";
        public override string Author { get; } = "Rohith M";

        public override bool Init()
        {
            // Add a menu item to start listening
            var rootbut = new ToolStripMenuItem("Start GPS RAW Listener");
            rootbut.Click += but_Click;
            Host.FDMenuMap.Items.Add(rootbut);
            return true;
        }

        private void but_Click(object sender, EventArgs e)
        {
            // subscribe to packets
            MainV2.comPort.OnPacketReceived -= OnComPortOnOnPacketReceived;
            MainV2.comPort.OnPacketReceived += OnComPortOnOnPacketReceived;
            Console.WriteLine("Listen for GPS_RAW_INT messages...");
        }

        private void OnComPortOnOnPacketReceived(object sender, MAVLink.MAVLinkMessage message)
        {
            if ((MAVLink.MAVLINK_MSG_ID)message.msgid == MAVLink.MAVLINK_MSG_ID.GPS_RAW_INT)
            {
                var gps = (MAVLink.mavlink_gps_raw_int_t)message.data;

                double lat = gps.lat / 1e7;
                double lon = gps.lon / 1e7;
                double alt = gps.alt / 1000.0; // mm to meters

                Console.WriteLine($"[GPS_RAW_INT] Lat: {lat:F7}, Lon: {lon:F7}, Alt: {alt:F1} m, Satellites: {gps.satellites_visible}");
            }
        }

        public override bool Loaded()
        {
            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        public override bool Loop()
        {
            return true;
        }
    }
}
