using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using UnityEngine.Internal;
[System.Serializable]
public class CourseLevels_Net
{
    [ShowInInspector]
    public List<Levels> items;

    [System.Serializable]
    public class Levels
    {
        public int id;

        [JsonProperty]
        List<Lesson> lessons = new List<Lesson>();

        [ShowInInspector]
        public List<Lesson> lessonList
        {
            get
            {
                if (lessons == null) return new List<Lesson>();
                return lessons;
            }
        }
    }


    [System.Serializable]
    public struct Lesson
    {
        public int id;

        public string name;

        public int courseId;

        public string icon;

        public bool locked => false;
    }

}



