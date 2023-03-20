
using System.Collections.Generic;
using Characters;
using UnityEngine;

public static class Utilits
{
    public static List<Enemy> AllEnemies;
    
    public static void Flip(Transform toFlip, float directionXPos)
    {
        var scale = toFlip.localScale;
        if (directionXPos < 0)
        {
            if (scale.x < 0) return;
            toFlip.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
        else
        {
            if (scale.x > 0) return;
            toFlip.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
    }
}
