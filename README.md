# MissionPlanner Plugin Example

This repository contains a **toy plugin** for [Mission Planner](https://ardupilot.org/planner/), showing how to extend its functionality with C#.  
The example plugin adds a simple UI to enter a latitude and longitude, then places a marker on the map.

---

## Files

- **`Test.cs`** – Example plugin script.  
  - Adds Lat/Lon input boxes and a "Set Marker" button in the Flight Data view.  
  - Places a red marker at the specified coordinates.  
  - Recenters the map on the new marker.

---

## Installation

1. Locate your Mission Planner installation folder.  
2. Copy `Test.cs` into the `MissionPlanner/plugins/` directory.  
3. Restart Mission Planner.  
4. Open the plugin manager with **CTRL + P** and enable the plugin.

---

## 🚀 Usage

1. Enter **Latitude** and **Longitude** into the provided text fields.  
   - Use `,` as the decimal separator (e.g. `57.234567`).  
2. Click **Set Marker**.  
3. A red marker will appear on the Mission Planner map at the given position.

![Demo of plugin](demo.gif)
