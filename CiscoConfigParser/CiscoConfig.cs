// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CiscoConfig.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Cisco Config File
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Cisco Config File
    /// </summary>
    public class CiscoConfig
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CiscoConfig"/> class.
        /// </summary>
        /// <param name="filename">
        /// The filename. 
        /// </param>
        public CiscoConfig(string filename)
            : this()
        {
            var lines = File.ReadAllLines(filename);
            this.Parse(lines);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CiscoConfig" /> class.
        /// </summary>
        public CiscoConfig()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets root section
        /// </summary>
        public Section Root { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="predicate">
        /// The predicate. 
        /// </param>
        /// <returns>
        /// All the sections that match the predicate 
        /// </returns>
        public IEnumerable<Section> Filter(Func<string, bool> predicate)
        {
            return this.Root.Filter(predicate);
        }

        /// <summary>
        /// Get sections that start with line.
        /// </summary>
        /// <param name="line">
        /// The line. 
        /// </param>
        /// <returns>
        /// Filtered sections 
        /// </returns>
        public IEnumerable<Section> FilterWith(string line)
        {
            return this.Filter(l => l.StartsWith(line));
        }

        /// <summary>
        /// The parse method.
        /// </summary>
        /// <param name="lines">
        /// The lines. 
        /// </param>
        public void Parse(IEnumerable<string> lines)
        {
            var lineCleaner = new LineCleaner();
            var sectionDelim = new SectionDelimiter();
            var indentChecker = new IndentChecker();
            var root = new Section();

            var current = root;
            int currentIndent = 0;

            foreach (var rawLine in lines)
            {
                var line = lineCleaner.Clean(rawLine);

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (sectionDelim.IsSectionDelimiter(line))
                {
                    current = root;
                    currentIndent = 0;
                    continue;
                }

                var indent = indentChecker.GetIndentLevel(line);

                if (indent > currentIndent)
                {
                    current = current.Children.Last();
                }

                if (indent < currentIndent)
                {
                    current = current.Head;
                }

                currentIndent = indent;
                current.AddChild(line);
            }

            this.Root = root;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The to string 
        /// </returns>
        public override string ToString()
        {
            return this.Root.ToString();
        }

        #endregion
    }
}