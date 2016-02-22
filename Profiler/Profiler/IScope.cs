namespace Profiler
{
    public interface IScope
    {
        /// <summary>
        /// Start the scope
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the scope
        /// </summary>
        void Stop();
    }
}
