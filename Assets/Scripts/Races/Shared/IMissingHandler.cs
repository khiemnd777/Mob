using System;

namespace Mob
{
	public interface IMissingHandler
	{
		void HandleMissing(float accuracy, float damage, Race target);
	}
}

