namespace SDF.Controll
{
    public class Head : SDFNode
    {
        [Output(connectionType = ConnectionType.Override)] public SDFNode nextNode;
    }
}