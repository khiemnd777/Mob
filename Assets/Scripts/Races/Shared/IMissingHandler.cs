using System;

namespace Mob
{
	public interface IMissingHandler
	{
		void HandleMissing(float accuracy, Race target);
	}
}

