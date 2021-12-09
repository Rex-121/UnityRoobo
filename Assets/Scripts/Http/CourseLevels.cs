using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using UnityEngine.Internal;
using Newtonsoft.Json.Linq;

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

        [JsonProperty]
        JToken package;

        [ShowInInspector]
        public List<ForgeData.Rounds> rounds
        {
            get
            {
                if (package == null) return new List<ForgeData.Rounds>();
                try
                {
                    return package.ToObject<List<ForgeData.Rounds>>();
                }
                catch
                {
                    return new List<ForgeData.Rounds>();
                }
            }
        }

        //[ShowInInspector]
        //public RoundIsPlaying rounding
        //{
        //    get
        //    {
        //        return Merge();
        //    }
        //}




        public RoundIsPlaying Merge()
        {

            var rList = rounds;

            List<Round> r = new List<Round>(rList.Count);

            foreach (var v in rList)
            {
                r.Add(new Round(v));
            }

            //RoundQueue

            List<RoundIsPlaying> play = new List<RoundIsPlaying>();

            foreach (var i in r)
            {
                play.AddRange(i.playlist);
            }


            LeadingRound leading = new LeadingRound();


            RoundIsPlaying start = leading;

            foreach (RoundIsPlaying roundIsPlaying in play)
            {
                roundIsPlaying.previous = start;
                start.next = roundIsPlaying;

                start = roundIsPlaying;
            }

            return leading.next;
        }
    }

}



