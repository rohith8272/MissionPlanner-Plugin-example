using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MissionPlanner.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.plugins.MapMarker
{
    public class MapMark : MissionPlanner.Plugin.Plugin
    {
        private TextBox inputLat;
        private TextBox inputLng;
        private Button setPositionBtn;
        private GMapOverlay markerOverlay;
        private GMarkerGoogle currentMarker;

        public override string Name => "Simple Map Marker";
        public override string Version => "0.1";
        public override string Author => "Rohith M";

       public override bool Loaded()
        {
            // Labels
            var labelLat = new Label { Text = "Lat:", AutoSize = true };
            var labelLng = new Label { Text = "Lon:", AutoSize = true };

            // Input boxes
            inputLat = new TextBox { Width = 80, Text = "37.7749" }; // Example default
            inputLng = new TextBox { Width = 80, Text = "-122.4194" };
            setPositionBtn = new Button { Text = "Set Marker", Width = 90 };

            // Find quick panel
            var quickPanelObj = Host.MainForm.FlightData.Controls.Find("tableLayoutPanelQuick", true)[0] as TableLayoutPanel;

            // Add controls in rows
            quickPanelObj.Controls.Add(labelLat, 0, quickPanelObj.RowCount);
            quickPanelObj.Controls.Add(inputLat, 1, quickPanelObj.RowCount);

            quickPanelObj.Controls.Add(labelLng, 0, quickPanelObj.RowCount + 1);
            quickPanelObj.Controls.Add(inputLng, 1, quickPanelObj.RowCount + 1);

            quickPanelObj.Controls.Add(setPositionBtn, 0, quickPanelObj.RowCount + 2);
            quickPanelObj.SetColumnSpan(setPositionBtn, 2); 

            // Overlay
            markerOverlay = new GMapOverlay("markers");
            Host.FDGMapControl.Overlays.Add(markerOverlay);

            // Button handler
            setPositionBtn.Click += (s, e) =>
            {
                double lat, lng;
                if (double.TryParse(inputLat.Text, out lat) && double.TryParse(inputLng.Text, out lng))
                {
                    SetMapMarker(lat, lng);
                }
            };

            return true;
}

        private void SetMapMarker(double lat, double lng)
        {
            // Remove previous marker
            if (markerOverlay == null)
            {
                markerOverlay = new GMapOverlay("markers");
                Host.FDGMapControl.Overlays.Add(markerOverlay);
            }
            markerOverlay.Markers.Clear();
            var point = new PointLatLng(lat, lng);
            currentMarker = new GMarkerGoogle(point, GMarkerGoogleType.red);
            markerOverlay.Markers.Add(currentMarker);
            Host.FDGMapControl.Position = point;
            Host.FDGMapControl.Refresh();
        }

        public override bool Init() => true;
        public override bool Loop() => true;
        public override bool Exit() => true;
    }
}
