/*
 * Magic Cloud, copyright Aista, Ltd. See the attached LICENSE file for details.
 */

using magic.node;
using builder = magic.data.common.builders;

namespace magic.lambda.sqlite.crud.builders
{
    /// <summary>
    /// Specialised delete SQL builder, to create a delete MySQL SQL statement
    /// by semantically traversing an input node.
    /// </summary>
    public class SqlDeleteBuilder : builder.SqlDeleteBuilder
    {
        /// <summary>
        /// Creates a delete SQL statement
        /// </summary>
        /// <param name="node">Root node to generate your SQL from.</param>
        public SqlDeleteBuilder(Node node)
            : base(node, "\"")
        { }
    }
}
