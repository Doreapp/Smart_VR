using System.Collections;
using System;
using UnityEngine;

public static class StaticCoordinates{
    [Serializable]
    public class Data
    {
        public Mode[] modes;
        public Map[] maps;
    }

    [Serializable]
    public class Mode
    {
        public string name;
    }

    [Serializable]
    public class Map
    {
        public string name,country;
        public Ball[] balls;
        public float lat,lon;
    }

    [Serializable]
    public class Ball
    {
        public string name;
        public float lat,lon;
    }

    public static int CityNumber{get;set;}
    public static Map[] maps;
    public static Mode[] modes;
    public static int SelectedCity = 0;
    public static int SelectedMode = 1;

    public static Map GetMap(){
        return maps[SelectedCity];
    }

    public static Mode GetMode(){
        return modes[SelectedMode];
    }

    public static int GetSelectedMode(){
        return SelectedMode;
    }

    public static void FecthData(){
        TextAsset file = Resources.Load<TextAsset>("Maps/Data");
        Data data = JsonUtility.FromJson<Data>(file.text);
        StaticCoordinates.maps = data.maps;
        StaticCoordinates.modes = data.modes;    
    }
}