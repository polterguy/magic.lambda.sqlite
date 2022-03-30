/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using System.Threading.Tasks;
using magic.node;
using magic.signals.contracts;
using magic.data.common.helpers;
using magic.lambda.sqlite.helpers;
using magic.data.common.contracts;

namespace magic.lambda.sqlite
{
    /// <summary>
    /// [sqlite.connect] slot for connecting to a PostgreSQL server instance.
    /// </summary>
    [Slot(Name = "sqlite.connect")]
    public class Connect : ISlot, ISlotAsync
    {
        readonly IDataSettings _settings;

        /// <summary>
        /// Creates a new instance of your class.
        /// </summary>
        /// <param name="configuration">Configuration for your application.</param>
        public Connect(IDataSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            using (var connection = new SqliteConnectionWrapper(
                Executor.GetConnectionString(
                    input,
                    "sqlite",
                    "sys",
                    _settings)))
            {
                signaler.Scope(
                    "sqlite.connect",
                    connection,
                    () => signaler.Signal("eval", input));
                input.Value = null;
            }
        }

        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        /// <returns>An awaitable task.</returns>
        public async Task SignalAsync(ISignaler signaler, Node input)
        {
            using (var connection = new SqliteConnectionWrapper(
                Executor.GetConnectionString(
                    input,
                    "sqlite",
                    "sys",
                    _settings)))
            {
                await signaler.ScopeAsync(
                    "sqlite.connect",
                    connection,
                    async () => await signaler.SignalAsync("eval", input));
                input.Value = null;
            }
        }
    }
}
