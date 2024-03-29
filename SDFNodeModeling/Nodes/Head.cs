using UnityEngine;

namespace SDF.Controll
{
    [NodeWidth(250)]
    [NodeTint("#ff0000")]
    public class Head : SDFNode
    {
        public Noise noise = Noise.none;
        public enum Noise {none, fog, Perlin}
        public Shadow shadow = Shadow.phong;
        public enum Shadow { phong, none}
        [Output(connectionType = ConnectionType.Override)] public SDFNode nextNode;

        /*
        public override string Calcsd(int i)
        {
            Debug.Log("Write a Head Code");
            string str = "dist = dist" + --i + ";";
            return str;
        }
        */
    }
}