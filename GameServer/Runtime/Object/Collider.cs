using System.Numerics;

public class Collider
{
    public Vector3 Center { get; set; }
    public Vector3 Scale { get; set; }
    public bool Enabled { get; set; }

    public Collider(bool isEnabled = true)
    {
        Enabled = isEnabled;
    }
}