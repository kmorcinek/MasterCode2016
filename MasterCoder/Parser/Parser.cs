// I spotted a bug during a weekend so I had enought time to play with this
// so tests are developed even too much

using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class Parser : IParser
    {
        const string BeginConstant = "BEGIN:VCARD";
        public const string FinishConstant = "END:VCARD";

        IParserListener _listener;
        string _leftover;

        public new static IParser GetInstance()
        {
            m_parserInstance = new Parser();
            return m_parserInstance;
        }

        public override ParseStatus parse(string buffer)
        {
            string dataToParse = buffer;

            if (string.IsNullOrEmpty(_leftover) == false)
            {
                dataToParse = _leftover + dataToParse;
                _leftover = null;
            }

            dataToParse = NormalizeFinishedEndLine(dataToParse);
            List<string> vCardBuffers = SplitAfterEachFinish(dataToParse);

            foreach (var vCardUnparsed in vCardBuffers)
            {
                // is not finished OR expect next part
                if (vCardUnparsed.Contains(FinishConstant) == false)
                {
                    _leftover = vCardUnparsed;
                    return ParseStatus.PARSE_STATUS_MORE_DATA;
                }

                if (string.IsNullOrEmpty(vCardUnparsed))
                {
                    continue;
                }

                ParseProperVCard(vCardUnparsed);
            }

            return ParseStatus.PARSE_STATUS_OK;
        }

        public static List<string> SplitAfterEachFinish(string buffer)
        {
            List<string> list = new List<string>();
            while (true)
            {
                var indexOf = buffer.IndexOf(FinishConstant, StringComparison.Ordinal);

                if (indexOf > 0)
                {
                    var whereToCut = indexOf + FinishConstant.Length;
                    list.Add(buffer.Substring(0, whereToCut));
                    buffer = buffer.Substring(whereToCut);
                }
                else
                {
                    if (buffer != "")
                    {
                        list.Add(buffer);
                    }
                    break;
                }
            }

            return list;
        }

        static string NormalizeFinishedEndLine(string buffer)
        {
            return buffer.Replace(FinishConstant + NewLine, FinishConstant);
        }

        void ParseProperVCard(string buffer)
        {
            var photoSb = new StringBuilder();

            var vCard = new vCardStruct();

            string[] parts = buffer.Split(new[] { NewLine }, StringSplitOptions.None);
            foreach (var part in parts)
            {
                if (part == BeginConstant) continue;
                if (part == FinishConstant) continue;

                // Parse N:data
                string[] nameAndData = part.Split(new[] { ":" }, StringSplitOptions.None);
                if (nameAndData.Length == 2)
                {
                    var name = nameAndData[0];
                    var data = nameAndData[1];

                    switch (name)
                    {
                        case "N":
                            vCard.Name = data;
                            break;
                        case "ADR":
                            vCard.Address = data;
                            break;
                        case "TEL":
                            vCard.TelNumber = data;
                            break;
                        case "EMAIL":
                            vCard.Email = data;
                            break;
                        case "PHOTO":
                            photoSb.Append(data);
                            break;
                    }
                }
                else
                {
                    var justData = nameAndData[0];

                    // not this empty line after photo
                    if (string.IsNullOrEmpty(justData) == false)
                    {
                        photoSb.Append(justData);
                    }
                }
            }

            vCard.PhotoData = photoSb.ToString();

            _listener.newVCard(new vCardStruct(vCard));
        }

        public override void setParserListener(IParserListener listener)
        {
            _listener = listener;
        }

        public const string NewLine = "\r\n";

        public class PhotoObject
        {
        }
    }
}