/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using System.Linq;
using System.Threading.Tasks;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.pgsql.helpers;
using magic.lambda.pgsql.crud.builders;
using help = magic.data.common.helpers;
using build = magic.data.common.builders;

namespace magic.lambda.pgsql.crud
{
    /// <summary>
    /// The [pgsql.create] slot class
    /// </summary>
    [Slot(Name = "pgsql.create")]
    public class Create : ISlot, ISlotAsync
    {
        /// <summary>
        /// Handles the signal for the class.
        /// </summary>
        /// <param name="signaler">Signaler used to signal the slot.</param>
        /// <param name="input">Root node for invocation.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            /*
             * Checking if caller wants us to return the ID of the newly
             * create record.
             */
            var returnId = input.Children
                .FirstOrDefault(x => x.Name == "return-id")?.GetEx<bool>() ?? true;

            // Parsing and creating SQL.
            var exe = returnId ?
                build.SqlBuilder.Parse(new SqlCreateBuilder(input)) :
                build.SqlBuilder.Parse(new SqlCreateBuilderNoId(input));

            /*
             * Notice, if the builder doesn't return a node, we are
             * not supposed to actually execute the SQL, but rather only
             * to generate it.
             */
            if (exe == null)
                return;

            // Executing SQL, now parametrized.
            help.Executor.Execute(
                exe,
                signaler.Peek<PgSqlConnectionWrapper>("pgsql.connect").Connection,
                signaler.Peek<help.Transaction>("pgsql.transaction"),
                (cmd, _) =>
            {
                /*
                 * Checking if caller wants us to return the ID of the newly
                 * created record.
                 */
                if (returnId)
                {
                    input.Value = cmd.ExecuteScalar();
                }
                else
                {
                    cmd.ExecuteNonQuery();
                    input.Value = null;
                }
                input.Clear();
            });
        }

        /// <summary>
        /// Implementation of your slot.
        /// </summary>
        /// <param name="signaler">Signaler used to raise the signal.</param>
        /// <param name="input">Arguments to your slot.</param>
        /// <returns>An awaitable task.</returns>
        public async Task SignalAsync(ISignaler signaler, Node input)
        {
            /*
             * Checking if caller wants us to return the ID of the newly
             * create record.
             */
            var returnId = input.Children
                .FirstOrDefault(x => x.Name == "return-id")?.GetEx<bool>() ?? true;

            // Parsing and creating SQL.
            var exe = returnId ?
                build.SqlBuilder.Parse(new SqlCreateBuilder(input)) :
                build.SqlBuilder.Parse(new SqlCreateBuilderNoId(input));

            /*
             * Notice, if the builder doesn't return a node, we are
             * not supposed to actually execute the SQL, but rather only
             * to generate it.
             */
            if (exe == null)
                return;

            // Executing SQL, now parametrized.
            await help.Executor.ExecuteAsync(
                exe,
                signaler.Peek<PgSqlConnectionWrapper>("pgsql.connect").Connection,
                signaler.Peek<help.Transaction>("pgsql.transaction"),
                async (cmd, _) =>
            {
                /*
                 * Checking if caller wants us to return the ID of the newly
                 * created record.
                 */
                if (returnId)
                {
                    input.Value = await cmd.ExecuteScalarAsync();
                }
                else
                {
                    await cmd.ExecuteNonQueryAsync();
                    input.Value = null;
                }
                input.Clear();
            });
        }
    }
}
