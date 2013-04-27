using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeGenerator
{
    public class ClassMaker
    {
        public static void MakeClass(string directory, string className, Dictionary<string, string> dic)
        {
            //if (className.EndsWith("s"))
            //{
            //    className = className.Substring(0, className.Length - 1);
            //}
            //else if (className.EndsWith("es"))
            //{
            //    className = className.Substring(0, className.Length - 2);
            //}
            //else if (className.EndsWith("ies"))
            //{
            //    className = className.Substring(0, className.Length - 3) + "y";
            //}

            using (TextWriter tw = new StreamWriter(string.Format("{0}\\{1}.cs", directory, className), false))
            {
                tw.WriteLine("using System;");
                tw.WriteLine();
                tw.WriteLine(string.Format("public class {0}", className));
                tw.WriteLine("{");

                foreach (KeyValuePair<string, string> s in dic)
                {
                    string type = string.Empty;
                    string baseType = (s.Value.Contains("(")) ? s.Value.Substring(0, s.Value.IndexOf("(")) : s.Value;

                    switch (baseType)
                    {
                        case "bigint":
                            type = "Int64";
                            break;

                        case "int":
                            type = "Int32";
                            break;

                        case "varbinary":
                        case "rowversion":
                        case "binary":
                            type = "byte[]";
                            break;

                        case "bit":
                            type = "bool";
                            break;

                        case "date":
                        case "datetime":
                        case "datetime2":
                            type = "DateTime";
                            break;

                        case "smallmoney":
                        case "numeric":
                        case "money":
                        case "decimal":
                            type = "decimal";
                            break;

                        case "float":
                            type = "double";
                            break;

                        case "real":
                            type = "Single";
                            break;

                        case "smallint":
                            type = "Int16";
                            break;

                        case "sql_variant":
                            type = "object";
                            break;

                        case "time":
                            type = "TimeSpan";
                            break;

                        case "tinyint":
                            type = "byte";
                            break;

                        case "uniqueidentifier":
                            type = "Guid";
                            break;

                        case "char":
                        case "nchar":
                        case "nvarchar":
                        default:
                            type = "string";
                            break;
                    }

                    tw.WriteLine(string.Format("\tpublic {0} {1};", type, s.Key));
                }

                tw.WriteLine("}");
            }
        }
    }
}
