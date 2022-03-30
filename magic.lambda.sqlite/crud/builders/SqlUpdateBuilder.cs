/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using magic.node;
using builder = magic.data.common.builders;

namespace magic.lambda.pgsql.crud.builders
{
    /// <summary>
    /// Specialised update SQL builder, to create a select MySQL SQL statement
    /// by semantically traversing an input node.
    /// </summary>
    public class SqlUpdateBuilder : builder.SqlUpdateBuilder
    {
        /// <summary>
        /// Creates an update SQL statement
        /// </summary>
        /// <param name="node">Root node to generate your SQL from.</param>
        public SqlUpdateBuilder(Node node)
            : base(node, "\"")
        { }
    }
}
