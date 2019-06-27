using UnityEngine;
using Pixeye.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Space
{
    
    sealed class ComponentOrbit 
  	{
        public LineRenderer view;
        public List<Vector2> orbitPoints = new List<Vector2>(100);
        public float deltaTime = 1f;
        public int maxPoints = 100;

    }

    #region HELPERS
    static partial class Components
    {
    
            public const string Orbit = "Space.ComponentOrbit";
    
        	[RuntimeInitializeOnLoadMethod]
    		static void ComponentOrbitInit()
    		{
    			Storage<ComponentOrbit>.Instance.Creator = () => { return new ComponentOrbit(); };
    			Storage<ComponentOrbit>.Instance.DisposeAction = DisposeComponentOrbit;
    		}
    
            /// Use this method to clean variables
        	[MethodImpl(MethodImplOptions.AggressiveInlining)]
    		internal static void DisposeComponentOrbit(in ent entity)
    		{
    	      ref var component = ref Storage<ComponentOrbit>.Instance.components[entity.id];
    		}
                
    		 
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static ref ComponentOrbit ComponentOrbit(in this ent entity)
            {
               return ref Storage<ComponentOrbit>.Instance.components[entity.id];
            }
    }
    #endregion
 
}
