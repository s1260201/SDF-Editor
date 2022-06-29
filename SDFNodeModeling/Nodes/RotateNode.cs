using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll
{
    public class RotateNode : SDFNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Input] public float rotation;
        
        public Axis axis = Axis.xy;
        public enum Axis { xy,yz,xz}

        [Output] public SDFNode node;

        //https://sayachang-bot.hateblo.jp/entry/2019/08/10/154208 ‰ñ“]‚³‚¹‚é

        public string rotateObj()
        {
            string str;
            str = "rot(p.";
            return str;
        }

    }

}