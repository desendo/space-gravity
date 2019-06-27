using UnityEngine;
using Pixeye.Framework;
using System.Runtime.CompilerServices;


namespace Space
{
    
    sealed class ComponentRigid 
  	{
        public Rigidbody2D source;
        public Vector2 force = Vector2.zero;
        public ent orbitParent;

    }

    #region HELPERS
    static partial class Components
    {
    
            public const string Rigid = "Space.ComponentRigid";
    
        	[RuntimeInitializeOnLoadMethod]
    		static void ComponentRigidInit()
    		{
    			Storage<ComponentRigid>.Instance.Creator = () => { return new ComponentRigid(); };
    			Storage<ComponentRigid>.Instance.DisposeAction = DisposeComponentRigid;
    		}
    
            /// Use this method to clean variables
        	[MethodImpl(MethodImplOptions.AggressiveInlining)]
    		internal static void DisposeComponentRigid(in ent entity)
    		{
    	      ref var component = ref Storage<ComponentRigid>.Instance.components[entity.id];
    		}
                
    		 
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static ref ComponentRigid ComponentRigid(in this ent entity)
            {
               return ref Storage<ComponentRigid>.Instance.components[entity.id];
            }
    }
    #endregion
 
}
