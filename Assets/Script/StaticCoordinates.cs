using System.Collections;
using System;
using UnityEngine;

public static class StaticCoordinates{
    [Serializable]
    public class Maps
    {
        public Map[] maps;
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

    public static void FecthMaps(){
        TextAsset file = Resources.Load<TextAsset>("Maps/AllMaps");
        Maps maps = JsonUtility.FromJson<Maps>(file.text);
        foreach(Map map in maps.maps){
            Debug.Log($"map: {map.name} in {map.country}");
        }
    }
}