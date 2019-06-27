using Pixeye.Framework;
using UnityEngine;

namespace Space
{
    public class StarterGame : Starter
    {
        protected override void Setup()
        {

            Add<ProcessorAttract>();

            Actor.Create("planet", Models.Planet).transform.position = Vector3.zero;

            for (int i = 0; i < 50; i++)
            {
                Vector2 t = Random.insideUnitCircle;
                t.Normalize();
                float v = Random.Range(20f, 50f);
                Actor.Create("asteroid", Models.Asteroid).transform.position =new Vector2( t.x * v, t.y * v);
            }
            

        }
    }
}