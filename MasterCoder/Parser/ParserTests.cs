using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;
using Moq;
using Xunit;

namespace Parser
{
    // natomiast znacznik końca może, ale nie musi. 
    // podzielony \r\n
    public class ParserTests
    {
        readonly Parser _sut;

        public ParserTests()
        {
            _sut = new Parser();
        }

        [Fact]
        public void Test()
        {
            var buffer = GetCorrectVCardWithoutEndLine() + Parser.NewLine + GetCorrectVCardWithoutEndLine() + "Konik";

            CutInAllPlaces(buffer);

            buffer = GetCorrectVCardWithoutEndLine() + GetCorrectVCardWithoutEndLine() + "Konik";

            CutInAllPlaces(buffer);
        }

        [Fact]
        public void TestSpecialBug()
        {
            var buffer = GetCorrectVCardWithoutEndLine() + Parser.NewLine + GetCorrectVCardWithoutEndLine();
            List<string> parts = CutIn(buffer, 1, 311);

            CountOnlyCorrectVCards(parts.ToArray(), 2);
        }

        [Fact]
        public void TestStartingNewLine()
        {
            var buffer = Parser.NewLine + GetCorrectVCardWithoutEndLine() + "Konik";
            List<string> parts = CutIn(buffer, 1, 2);

            CountOnlyCorrectVCards(parts.ToArray(), 1);
        }

        void CutInAllPlaces(string inputBuffer)
        {
            var startingIndex = GetCorrectVCardWithoutEndLine().Length - 2;
            for (int i = startingIndex; i < inputBuffer.Length; i++)
            {
                for (int j = i + 1; j < startingIndex + 32; j++)
                {
                    List<string> parts = CutIn(inputBuffer, i, j - i);

                    CountOnlyCorrectVCards(parts, 2);
                }
            }
        }

        public void CountOnlyCorrectVCards(IEnumerable<string> parts, int vcardsCount)
        {
            var mock = new Mock<IParserListener>();

            bool called = false;
            _sut.setParserListener(mock.Object);

            mock.Setup(x => x.newVCard(It.IsAny<vCardStruct>())).Callback((vCardStruct arg) =>
            {
                AssertVCard(GetCorrectVCard(), arg);
                called = true;
            });

            foreach (var part in parts)
            {
                _sut.parse(part);
            }

            mock.Verify(x => x.newVCard(It.IsAny<vCardStruct>()), Times.Exactly(vcardsCount));
        }

        List<string> CutIn(string inputBuffer, int firstLenght, int secondLength)
        {
            List<string> parts = new List<string>(3);

            var stringWrapper = new StringWrapper(inputBuffer);
            parts.Add(stringWrapper.Substring(firstLenght));

            parts.Add(stringWrapper.Substring(secondLength));

            parts.Add(stringWrapper.GetRest());

            Assert.Equal(inputBuffer.Length, parts.Sum(x => x.Length));

            return parts;
        }

        class StringWrapper
        {
            readonly string _startingValue;
            int _currentIndex;

            public StringWrapper(string startingValue)
            {
                _startingValue = startingValue;
            }

            public string Substring(int length)
            {
                var substring = _startingValue.Substring(_currentIndex, length);
                _currentIndex += length;

                return substring;
            }

            public string GetRest()
            {
                var substring = _startingValue.Substring(_currentIndex);
                _currentIndex = _startingValue.Length;

                return substring;
            }
        }

        [Fact]
        public void SplitWithout()
        {
            var c = '\n';
            int a = (int)c;
            var firstPart = "adf";
            var second = "konio";
            List<string> splitAfterEachFinish = Parser.SplitAfterEachFinish(firstPart + Parser.FinishConstant + second);

            Assert.Equal(firstPart + Parser.FinishConstant, splitAfterEachFinish[0]);
            Assert.Equal(second, splitAfterEachFinish[1]);
        }

        void AssertVCard(vCardStruct first, vCardStruct second)
        {
            Assert.Equal(first.Name, second.Name);
            Assert.Equal(first.Address, second.Address);
            Assert.Equal(first.Email, second.Email);
            Assert.Equal(first.PhotoData, second.PhotoData);
            Assert.Equal(first.TelNumber, second.TelNumber);
        }

        [Fact]
        public void SetCorrectBuffor()
        {
            string buffor = GetCorrectVCardWithoutEndLine();

            var mock = new Mock<IParserListener>();

            _sut.setParserListener(mock.Object);
            var status = _sut.parse(buffor);

            Assert.Equal(ParseStatus.PARSE_STATUS_OK, status);
            mock.Verify(x => x.newVCard(It.IsAny<vCardStruct>()), Times.Once());
        }

        [Fact]
        public void MixNewLinesWith_more_returned()
        {
            string first = GetCorrectVCardWithoutEndLine() + "\r";

            _sut.setParserListener(Mock.Of<IParserListener>());
            var status = _sut.parse(first);

            Assert.Equal(ParseStatus.PARSE_STATUS_MORE_DATA, status);
        }

        [Fact]
        public void MixNewLines()
        {
            string first = GetCorrectVCardWithoutEndLine() + "\r";
            string second = "\n" + GetCorrectVCardWithoutEndLine();

            InChunksMany(new[] { first, second }, 2);
        }

        public void InChunksMany(string[] args, int numberOfFullVCards)
        {
            var mock = new Mock<IParserListener>();

            _sut.setParserListener(mock.Object);

            foreach (var chunk in args)
            {
                _sut.parse(chunk);
            }

            mock.Verify(x => x.newVCard(It.IsAny<vCardStruct>()), Times.Exactly(numberOfFullVCards));
        }

        [Fact]
        public void SetOk_and_parsed_two_WhenExactTwoSendWithNewLine()
        {
            string buffer = GetCorrectVCardWithoutEndLine() + Parser.NewLine + GetCorrectVCardWithoutEndLine();

            VerifyExactly(buffer);
        }

        [Fact]
        public void SetOk_and_parsed_two_WhenExactTwoSendWithoutNewLine()
        {
            string buffor = GetCorrectVCardWithoutEndLine() + GetCorrectVCardWithoutEndLine();

            VerifyExactly(buffor);
        }

        public void VerifyExactly(string buffer)
        {
            var mock = new Mock<IParserListener>();

            bool called = false;

            mock.Setup(x => x.newVCard(It.IsAny<vCardStruct>())).Callback((vCardStruct arg) =>
            {
                AssertVCard(GetCorrectVCard(), arg);
                called = true;
            });

            _sut.setParserListener(mock.Object);
            var status = _sut.parse(buffer);

            Assert.Equal(ParseStatus.PARSE_STATUS_OK, status);
            mock.Verify(x => x.newVCard(It.IsAny<vCardStruct>()), Times.Exactly(2));
        }

        [Fact]
        public void SetOk_one_buffer_in_three_chunksSingle()
        {
            string fullBuffor = GetCorrectVCardWithoutEndLine();

            var whereToCut = 50;
            string firstBuffer = fullBuffor.Substring(0, whereToCut);
            string secondBuffer = fullBuffor.Substring(whereToCut, 50);
            string third = fullBuffor.Substring(100);

            Assert.Equal(fullBuffor, firstBuffer + secondBuffer + third);

            InChunks(new[] { firstBuffer, secondBuffer, third }, 2);
        }

        [Fact]
        public void Set_in_two_chunks_In_Finish()
        {
            var correctVCardWithoutEndLine = GetCorrectVCardWithoutEndLine();
            var indexOf = correctVCardWithoutEndLine.IndexOf(Parser.FinishConstant);
            Set_in_two_chunks(indexOf + 3);
        }

        [Fact]
        public void Set_in_two_chunks_InMiddle()
        {
            Set_in_two_chunks(50);
        }

        public void Set_in_two_chunks(int whereToCut)
        {
            string fullBuffor = GetCorrectVCardWithoutEndLine();

            string firstBuffer = fullBuffor.Substring(0, whereToCut);
            string secondBuffer = fullBuffor.Substring(whereToCut);

            Assert.Equal(fullBuffor, firstBuffer + secondBuffer);

            InChunks(new[] { firstBuffer, secondBuffer }, 1);
        }

        public void InChunks(string[] args, int parts)
        {
            var mock = new Mock<IParserListener>();

            bool called = false;
            vCardStruct vCard = new vCardStruct();
            _sut.setParserListener(mock.Object);

            mock.Setup(x => x.newVCard(It.IsAny<vCardStruct>())).Callback((vCardStruct arg) =>
            {
                called = true;
                vCard = arg;
            });

            for (int i = 0; i < parts; i++)
            {
                var status = _sut.parse(args[i]);
                Assert.Equal(ParseStatus.PARSE_STATUS_MORE_DATA, status);
                Assert.False(called);
            }

            var status2 = _sut.parse(args[parts]);

            Assert.Equal(ParseStatus.PARSE_STATUS_OK, status2);
            Assert.True(called);
            AssertVCard(GetCorrectVCard(), vCard);
        }

        [Fact]
        public void SetMoreWhenNotCompleted()
        {
            string buffor = "alamakota";

            var status = _sut.parse(buffor);

            Assert.Equal(ParseStatus.PARSE_STATUS_MORE_DATA, status);
        }

        static readonly Lazy<string> LazyVCard = new Lazy<string>(() =>
        {
            var sb = new StringBuilder();
            AppendWithEndLine(sb, "BEGIN:VCARD");
            AppendWithEndLine(sb, "N:Jan Kowalski");
            AppendWithEndLine(sb, "PHOTO:oijasdcokmasodijcocmoijasdcokmasodIjcocmoijasdcokmasodijcocmoijasdcokmasodijco");
            AppendWithEndLine(sb, "asodijcocmoijasdcokmasodijcocmoijasdcokmasOdijcocmoijasdcokmasodijcocmoijasdco");
            AppendWithEndLine(sb, "asodijcocmoijasdcokYYJrr");
            AppendWithEndLine(sb, "");
            AppendWithEndLine(sb, "TEL:696 969 696");
            AppendWithEndLine(sb, "ADR:ul. Dowborczykow 25, 90-993 Lodz");
            AppendWithEndLine(sb, "EMAIL:jan.kowalski@cybercom.com");
            // moze ale nie musi
            sb.Append("END:VCARD");

            return sb.ToString();
        });

        static string GetCorrectVCardWithoutEndLine()
        {
            return LazyVCard.Value;
        }

        static readonly Lazy<vCardStruct> LazyVCardStruct = new Lazy<vCardStruct>(() => new vCardStruct()
        {
            Address = "ul. Dowborczykow 25, 90-993 Lodz",
            Name = "Jan Kowalski",
            PhotoData = "oijasdcokmasodijcocmoijasdcokmasodIjcocmoijasdcokmasodijcocmoijasdcokmasodijcoasodijcocmoijasdcokmasodijcocmoijasdcokmasOdijcocmoijasdcokmasodijcocmoijasdcoasodijcocmoijasdcokYYJrr",
            TelNumber = "696 969 696",
            Email = "jan.kowalski@cybercom.com",
        });

        static vCardStruct GetCorrectVCard()
        {
            return LazyVCardStruct.Value;
        }

        static void AppendWithEndLine(StringBuilder sb, string data)
        {
            sb.Append(data + Parser.NewLine);
        }
    }
}