﻿using System;
using System.Management.Automation;

namespace Pester
{
    static class ToStringConverter
    {
        static string ResultToString(string result)
        {
            return result switch
            {
                "Passed" => "[+]",
                "Failed" => "[-]",
                "Skipped" => "[!]",
                "Inconclusive" => "[?]",
                "NotRun" => "[ ]",
                _ => "[ERR]",
            };
        }

        internal static string ContainerToString(Container container)
        {
            string path;
            switch (container.Type)
            {
                case Constants.File:
                    path = container.Item.ToString();
                    break;
                case Constants.ScriptBlock:
                    path = "<ScriptBlock>";
                    if (container.Item is ScriptBlock s) {
                        if (!string.IsNullOrWhiteSpace(s.File))
                        {
                            path += $":{s.File}:{s.StartPosition.StartLine}";
                        }
                    }
                    break;
                default:
                    path = $"<{container.Type}>";
                    break;
            }

            return $"{ResultToString(container.Result)} {path}";
        }

        internal static string TestToString(Test test)
        {
            return $"{ResultToString(test.Result)} {test.ExpandedName ?? test.Name}";
        }

        internal static string BlockToString(Block block)
        {
            return $"{ResultToString(block.Result)} {block.Name}";
        }

        internal static string RunToString(Run run)
        {
            return $"{ResultToString(run.Result)} Pester";
        }
    }
}
