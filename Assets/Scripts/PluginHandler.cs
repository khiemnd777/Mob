
namespace Mob
{
	public abstract class PluginHandler : MonoHandler
	{
		public virtual void InitPlugin(){
			
		}
		public abstract void HandlePlugin(params object[] args);
	}
}

