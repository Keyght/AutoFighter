
using UnityEngine;

public static class Utilits
{
    public static void Flip(Transform toFlip, float directionXPos)
    {
        if (directionXPos < 0)
        {
            var scale = toFlip.localScale;
            toFlip.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
    }
}
