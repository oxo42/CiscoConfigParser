// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Section.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Section
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Config Section
    /// </summary>
    public class Section
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="head">
        /// The head. 
        /// </param>
        /// <param name="line">
        /// The line. 
        /// </param>
        public Section(Section head, string line)
            : this()
        {
            this.Head = head;
            this.Line = line.Trim();
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Section" /> class.
        /// </summary>
        public Section()
        {
            this.Head = null;
            this.Line = null;
            this.Children = new List<Section>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Children.
        /// </summary>
        public List<Section> Children { get; set; }

        /// <summary>
        ///   Gets or sets Head.
        /// </summary>
        public Section Head { get; set; }

        /// <summary>
        ///   Gets or sets Line.
        /// </summary>
        public string Line { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Add a chile to this section
        /// </summary>
        /// <param name="line">
        /// The line. 
        /// </param>
        /// <returns>
        /// The chile added to the section 
        /// </returns>
        public Section AddChild(string line)
        {
            var child = new Section(this, line);
            this.Children.Add(child);
            return child;
        }

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
            if (!string.IsNullOrWhiteSpace(this.Line) && predicate(this.Line))
            {
                yield return this;
            }
            else
            {
                foreach (var child in this.Children)
                {
                    foreach (var section in child.Filter(predicate))
                    {
                        yield return section;
                    }
                }
            }
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
        /// The get token.
        /// </summary>
        /// <param name="number">
        /// The nth token.  This is a 1 based index
        /// </param>
        /// <returns>
        /// The nth token. 
        /// </returns>
        public string GetToken(int number)
        {
            return this.Line.Split(' ')[number - 1];
        }

        /// <summary>
        /// Has this section got a child matching the predicate
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// If the section has a child.
        /// </returns>
        public bool HasChild(Func<string, bool> predicate)
        {
            foreach (var child in this.Children)
            {
                if (child.Filter(predicate).Any())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Has this section got a child starting with line
        /// </summary>
        /// <param name="line">
        /// The line. 
        /// </param>
        /// <returns>
        /// If the section has a child.
        /// </returns>
        public bool HasChildWith(string line)
        {
            return this.HasChild(l => l.StartsWith(line));
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="indent">
        /// The indent. 
        /// </param>
        /// <returns>
        /// The section as a string. 
        /// </returns>
        public string ToString(string indent)
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(this.Line);
            sb.Append(Environment.NewLine);
            foreach (var child in this.Children)
            {
                sb.Append(child.ToString(indent + " "));
            }

            return sb.ToString();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The section as a string. 
        /// </returns>
        public override string ToString()
        {
            return this.ToString(string.Empty);
        }

        #endregion
    }
}