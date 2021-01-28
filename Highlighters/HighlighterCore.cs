﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreneticUtilities.FreneticExtensions;
using FreneticUtilities.FreneticToolkit;

namespace DenizenPastingWebsite.Highlighters
{
    /// <summary>
    /// Helper class for all highlighter types.
    /// </summary>
    public static class HighlighterCore
    {
        /// <summary>
        /// A helper matcher for characters that need HTML escaping.
        /// </summary>
        public static AsciiMatcher NeedsEscapeMatcher = new AsciiMatcher("&<>\0\t");

        /// <summary>
        /// Escapes some text to be safe to put into HTML.
        /// </summary>
        public static string EscapeForHTML(string text)
        {
            if (!NeedsEscapeMatcher.ContainsAnyMatch(text))
            {
                return text.Replace(" ", "&nbsp;");
            }
            return text.Replace("\0", " ").Replace("&", "&amp;").Replace("\t", "    ").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" ", "&nbsp;");
        }

        /// <summary>
        /// Formats plain text.
        /// </summary>
        public static string HighlightPlainText(string text)
        {
            text = EscapeForHTML(text);
            return HandleLines(text);
        }

        /// <summary>
        /// The final stage of most highlighters, turns newlines into HTML newlines and generates a line number sidebar.
        /// </summary>
        public static string HandleLines(string text)
        {
            int lineCount = text.CountCharacter('\n') + 2;
            StringBuilder lineNumbers = new StringBuilder(lineCount * 40);
            for (int i = 1; i < lineCount; i++)
            {
                lineNumbers.Append($"<a id=\"{i}\" href=\"#{i}\">{i}</a>\n");
            }
            return $"<div class=\"line_numbers\"><pre><code>\n{lineNumbers}\n</code></pre></div>\n<div class=\"paste_body\"><pre><code>\n{text}\n</code></pre></div>\n";
        }
    }
}
