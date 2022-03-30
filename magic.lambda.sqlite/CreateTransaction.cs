/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using magic.node;
using magic.signals.contracts;
using System.Threading.Tasks;
using magic.lambda.sqlite.helpers;
using hlp = magic.data.common.helpers;

namespace magic.lambda.sqlite
{
    /// <summary>
    /// [sqlite.transaction.create] slot for creating a new MySQL database transaction.
    /// </summary>
    [Slot(Name = "sqlite.transaction.create")]
    public class CreateTransaction : ISlot, ISlotAsync
    {
        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            signaler.Scope(
                "sqlite.transaction",
                new hlp.Transaction(signaler, signaler.Peek<SqliteConnectionWrapper>("sqlite.connect").Connection),
                () => signaler.Signal("eval", input));
        }

        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        /// <returns>An awaitable task.</returns>
        public async Task SignalAsync(ISignaler signaler, Node input)
        {
            await signaler.ScopeAsync(
                "sqlite.transaction",
                new hlp.Transaction(signaler, signaler.Peek<SqliteConnectionWrapper>("sqlite.connect").Connection),
                async () => await signaler.SignalAsync("eval", input));
        }
    }
}
