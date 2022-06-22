namespace SDF.Controll
{
    public class Head : SDFNode
    {
        public Noise noise = Noise.none;
        public enum Noise {none, fog, Perlin}
        public Shadow shadow = Shadow.phong;
        public enum Shadow { phong, none}
        [Output(connectionType = ConnectionType.Override)] public SDFNode nextNode;
    }
}