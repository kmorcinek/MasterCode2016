// I used C# 6.0 feature here cause I didn't know yet that we are on C# 5.0
// I also had couple classes instead of only one - also agains rules
// I also didn't implement GetInstance like in instruction

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using SQLConverter;
//
//namespace JustOneProject.Zadanie1
//{
//    /// <summary>
//    /// My assumptions:
//    /// * If we cannot parse int or DateTime, then insert NULL
//    /// </summary>
//    public class SQLConverter : ISQLConverter
//    {
//        public const string NewLine = "\r\n";
//        public const string Delimiter = ";";
//
//        public override List<SqlQuery> ConvertToSqlInsert(string tabName, string typesBuff, string colNamBuff, string dataBuff)
//        {
//            tabName = RemoveNewLine(tabName);
//            typesBuff = RemoveNewLine(typesBuff);
//            colNamBuff = RemoveNewLine(colNamBuff);
//
//            string[] lines = dataBuff.Split(new[] { NewLine }, StringSplitOptions.RemoveEmptyEntries);
//
//            string[] columnNames = ExtractColumnNames(colNamBuff);
//            IParser[] parsers = ExtractParsers(typesBuff);
//
//            var sqlQueries = new List<SqlQuery>();
//
//            foreach (var line in lines)
//            {
//                string[] unparsedValues = line.Split(new[] { Delimiter }, StringSplitOptions.None);
//                string[] outputValues = ParseValues(parsers, unparsedValues);
//                List<string> generateForOneValues = GenerateForOneValues(tabName, columnNames, outputValues);
//                sqlQueries.Add(new SqlQuery(generateForOneValues));
//            }
//
//            return sqlQueries;
//        }
//
//        private string[] ParseValues(IParser[] parsers, string[] unparsedValues)
//        {
//            var list = new List<string>();
//
//            for (int i = 0; i < parsers.Length; i++)
//            {
//                list.Add(parsers[i].ParseValue(unparsedValues[i]));
//            }
//
//            return list.ToArray();
//        }
//
//        private static IParser[] ExtractParsers(string typesBuff)
//        {
//            string[] typeNames = ExtractNamesByDelimiter(typesBuff);
//
//            var parsers = new List<IParser>();
//
//            foreach (var typeName in typeNames)
//            {
//                switch (typeName)
//                {
//                    case "STRING":
//                        parsers.Add(new StringParser());
//                        break;
//                    case "INT":
//                        parsers.Add(new IntParser());
//                        break;
//                    case "DATE":
//                        parsers.Add(new DateTimeParser());
//                        break;
//                }
//            }
//
//            return parsers.ToArray();
//        }
//
//        private string RemoveNewLine(string input)
//        {
//            return input.Replace(NewLine, string.Empty);
//        }
//
//        private static string[] ExtractNamesByDelimiter(string buffer)
//        {
//            string[] extractedNames = buffer.Split(new []{Delimiter}, StringSplitOptions.RemoveEmptyEntries);
//
//            return extractedNames;
//        }
//
//        private static string[] ExtractColumnNames(string colNamBuff)
//        {
//            var extractColumnNames = ExtractNamesByDelimiter(colNamBuff);
//
//            // Columns needs to be wrapped with this
//            return extractColumnNames.Select(x => $"[{x}]").ToArray();
//        }
//
//        static List<string> GenerateForOneValues(string tabName, string[] columnNames, string[] values)
//        {
//            var list = new List<string>();
//            list.Add("INSERT");
//            list.Add("INTO");
//            list.Add(tabName);
//            list.Add("(");
//
//            AddItemsWithComa(list, columnNames);
//            list.Add(")");
//            list.Add("VALUES");
//            list.Add("(");
//
//            AddItemsWithComa(list, values);
//
//            list.Add(")");
//            list.Add(";");
//
//            return list;
//        }
//
//        public static void AddItemsWithComa(List<string> list, string[] columnNames)
//        {
//            for (int i = 0; i < columnNames.Length; i++)
//            {
//                list.Add(columnNames[i]);
//                if (i + 1 < columnNames.Length)
//                {
//                    list.Add(",");
//                }
//            }
//        }
//    }
//
//    public interface IParser
//    {
//        string ParseValue(string input);
//    }
//
//    public class IntParser : IParser
//    {
//        public string ParseValue(string input)
//        {
//            int value;
//            if (int.TryParse(input, out value))
//            {
//                return input;
//            }
//
//            return "NULL";
//        }
//    }
//
//    public class DateTimeParser : IParser
//    {
//        public string ParseValue(string input)
//        {
//            DateTime value;
//            if (DateTime.TryParse(input, out value))
//            {
//                return $"'{input}'";
//            }
//
//            return "NULL";
//        }
//    }
//
//    public class StringParser : IParser
//    {
//        public string ParseValue(string input)
//        {
//            return $"'{input}'";
//        }
//    }
//}