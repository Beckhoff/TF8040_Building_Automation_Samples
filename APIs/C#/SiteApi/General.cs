using System;
using System.Threading.Tasks;
using TwinCAT.BA.Site;

namespace Beckhoff.BA.SiteApi.Samples
{
    #region Interfaces
    public interface ISample
    {
    }

    /// <summary>
    /// Independant sample that can be <see cref="Run">executed</see> regardless to any site's connection state.
    /// </summary>
    /// <remarks></remarks>
    public interface IGeneralSample : ISample
    {
        /// <summary>
        /// Demonstrational code to be executed <b>after</b> site connection is established.
        /// </summary>
        Task Run();
    }

    /// <summary>
    /// Sample that can be <see cref="Run">executed</see>.
    /// </summary>
    public interface IExecutableSample : ISample
    {
        /// <summary>
        /// Demonstrational code to be executed <b>after</b> site connection is established.
        /// </summary>
        Task Run();
    }
    /// <summary>
    /// Sample that can be <see cref="Initialize">initialized</see> and <see cref="IExecutableSample.Run">executed</see>.
    /// </summary>
    public interface IInitializableSample : IExecutableSample
    {
        /// <summary>
        /// Initializational code to be executed <b>before</b> site connection is established.
        /// </summary>
        void Initialize();
    }
    #endregion
    #region Types
    public abstract class Loader
    {
        protected static async Task RunSampleAsync(string sTestDevice, ISample sample)
        {
            if (sample is IGeneralSample iGen)
                await iGen.Run();

            else if (sample is IExecutableSample iExec)
            {
                BaSite.AddDevice(sTestDevice);

                if (sample is IInitializableSample iInit)
                    iInit.Initialize();

                // Establish site connection:
                await BaSite.ConnectAsync();
                {
                    await iExec.Run();
                }
                await BaSite.DisconnectAsync();
            }
            else
                throw new NotImplementedException($"Failed to load sample of not implemented type '{sample.GetType().Name}'!");
        }
    }
    #endregion
}
