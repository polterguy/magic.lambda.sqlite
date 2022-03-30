/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using System.Text;
using magic.node;
using builder = magic.data.common.builders;

namespace magic.lambda.sqlite.crud.builders
{
    /// <summary>
    /// Specialised insert SQL builder, to create an insert MySQL SQL statement
    /// by semantically traversing an input node.
    /// </summary>
    public class SqlCreateBuilder : builder.SqlCreateBuilder
    {
        /// <summary>
        /// Creates an insert SQL statement
        /// </summary>
        /// <param name="node">Root node to generate your SQL from.</param>
        public SqlCreateBuilder(Node node)
            : base(node, "\"")
        { }

        /// <summary>
        /// Overridden to generate the tail parts of your SQL.
        /// </summary>
        /// <param name="builder">Where to put your tail.</param>
        protected override void AppendTail(StringBuilder builder)
        {
            builder.Append(" returning *");
        }
    }
}
