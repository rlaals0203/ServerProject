using UnityEngine;

public class SpiritEffect : SpriteEffect
{
    [SerializeField] private float speed = 5f;

    protected override void Update()
    {
        base.Update();
        transform.position += new Vector3(0, speed, 0f) * Time.deltaTime;
    }
}