using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDF.Controll
{
    public class RotateNode : SDFNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public SDFNode beforeNode;
        [Input] public Vector3 rotation;

        //https://sayachang-bot.hateblo.jp/entry/2019/08/10/154208 ‰ñ“]‚³‚¹‚é
    }

}