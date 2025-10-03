using MissionPlanner.Controls;
using System;
using System.Windows.Forms;

namespace MissionPlanner.plugins.PanelExplorer
{
    public class PanelExplorer : MissionPlanner.Plugin.Plugin
    {
        public override string Name => "Panel Explorer";
        public override string Version => "0.1";
        public override string Author => "Rohith M";

        public override bool Init() => true;
        public override bool Loop() => true;

        public override bool Loaded()
        {
            // Print all controls inside FlightData tab
            Console.WriteLine("=== FlightData Controls ===");
            DumpControls(Host.MainForm.FlightData, "");

            return true;
        }

        public override bool Exit()
        {
            Console.WriteLine("Panel Explorer unloaded.");
            return true;
        }

        private void DumpControls(Control parent, string indent)
        {
            foreach (Control c in parent.Controls)
            {
                Console.WriteLine($"{indent}- {c.Name} ({c.GetType().Name})");

                if (c.HasChildren)
                {
                    DumpControls(c, indent + "  ");
                }
            }
        }
    }
}
