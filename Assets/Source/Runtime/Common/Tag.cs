//  Project : ecs
// Contacts : Pix - ask@pixeye.games

using Pixeye.Framework;

namespace Space
{
	public class Tag : ITag
	{
		[TagField]
		public const int None = 0;
        [TagField]
        public const int StartOrbit = 1;
        public const int OrbitGiver = 2;
    }
}