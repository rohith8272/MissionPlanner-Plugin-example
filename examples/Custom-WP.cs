using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MissionPlanner.Controls;
using System.Collections.Generic;
using System.Text;
using MissionPlanner.Utilities;

using System.Diagnostics;
using MissionPlanner;
using System.Drawing;

namespace Shortcuts
{
    public class Plugin : MissionPlanner.Plugin.Plugin
    {
        ToolStripMenuItem but;
        MissionPlanner.Controls.MyDataGridView commands;

        public override string Name { get { return "customWP"; } }
        public override string Version { get { return "0.1"; } }
        public override string Author { get { return "Rohith M"; } }

        public override bool Init() { return true; }
        public override bool Loop() { return true; }
        public override bool Exit() { return true; }

        public override bool Loaded()
        {
            but = new ToolStripMenuItem("Mission library");
            but.Click += but_Click;
            Host.FPMenuMap.Items.Add(but);

            commands = Host.MainForm.FlightPlanner.Controls
                .Find("Commands", true).FirstOrDefault() as MissionPlanner.Controls.MyDataGridView;

            return true;
        }

        void but_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Show("Load custom waypoints (common flight plans)");

           
            var customMission = new (double lat, double lon, double alt)[]
            {
                (47.397742, 8.545594, 50),   // WP1
                (47.397900, 8.546000, 60),   // WP2
                (47.398200, 8.546400, 70),   // WP3
                (47.398400, 8.546700, 50)    // WP4
            };

            // Clear current mission
            Host.MainForm.FlightPlanner.clearMissionToolStripMenuItem_Click(null, null);

            // Insert each WP 
            for (int i = 0; i < customMission.Length; i++)
            {
                var wp = customMission[i];
                Host.InsertWP(i, MAVLink.MAV_CMD.WAYPOINT,
                    0, 0, 0, 0,
                    wp.lat, wp.lon, wp.alt);
            }

            // Also add to the mission list UI
            foreach (var wp in customMission)
            {
                Host.AddWPtoList(MAVLink.MAV_CMD.WAYPOINT,
                    0, 0, 0, 0,
                    wp.lat, wp.lon, wp.alt);
            }


            if (commands.Rows.Count > 0)
                commands.Rows.RemoveAt(0);
        }
    }
}
