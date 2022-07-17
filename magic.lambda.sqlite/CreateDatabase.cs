﻿/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using Aista.Data.Sqlite;
using magic.node;
using magic.signals.contracts;

namespace magic.lambda.sqlite
{
    /// <summary>
    /// [sqlite.connections.flush] slot for flushing SQLite connections.
    /// </summary>
    [Slot(Name = "sqlite.connections.flush")]
    public class FlushConnections : ISlot
    {
        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            SqliteConnection.ClearAllPools();
        }
    }
}
