/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using Microsoft.Data.Sqlite;
using magic.node;
using magic.signals.contracts;

namespace magic.lambda.sqlite
{
    /// <summary>
    /// [.db-factory.connection.sqlite] slot for creating a PostgreSQL connection and returning to caller.
    /// </summary>
    [Slot(Name = ".db-factory.connection.sqlite")]
    public class ConnectionFactory : ISlot
    {
        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            input.Value = new SqliteConnection();
        }
    }
}
