//  Project : game.cryoshockmini
// Contacts : Pix - ask@pixeye.games

using System;
using Pixeye.Framework;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Space
{
	[Serializable]
	sealed class ComponentObject
	{
		public Vector3 position;
	}

    #region HELPERS
    static partial class Components
    {

        public const string Object = "Space.ComponentObject";

        [RuntimeInitializeOnLoadMethod]
        static void ComponentObjectInit()
        {
            Storage<ComponentObject>.Instance.Creator = () => { return new ComponentObject(); };
            Storage<ComponentObject>.Instance.DisposeAction = DisposeComponentObject;
        }

        /// Use this method to clean variables
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DisposeComponentObject(in ent entity)
        {
            ref var component = ref Storage<ComponentObject>.Instance.components[entity.id];
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref ComponentObject ComponentObject(in this ent entity)
        {
            return ref Storage<ComponentObject>.Instance.components[entity.id];
        }
    }
    #endregion

}