using System.Linq;

namespace Decoder
{
    public class Decoder : IDecoder
    {
        public new static IDecoder GetInstance()
        {
            decoderInstance = new Decoder();
            return decoderInstance;
        }

        public override Result decode(long searchedValue, string alphabet)
        {
            return decodeRecursive(searchedValue, alphabet);
        }

        public Result decodeRecursive(long searchedValue, string alphabet)
        {
            if (searchedValue == 3)
            {
                return new Result("", ResultCode.SUCCESS);
            }

            for (int i = 0; i < alphabet.Length; i++)
            {
                var beforeDividing = searchedValue - i;
                if (beforeDividing % 29 == 0)
                {
                    var currentChar = alphabet[i];

                    var result = decodeRecursive(beforeDividing / 29, alphabet);
                    if (result.CodeResult == ResultCode.FAILURE)
                    {
                        return FailureValue;
                    }

                    return new Result(result.DecodedText + currentChar, ResultCode.SUCCESS);
                }
            }

            return FailureValue;
        }

        private static Result FailureValue
        {
            get { return new Result("", ResultCode.FAILURE); }
        }

        public static long code(string what, string alphabet)
        {
            long value = 3;

            for (int i = 0; i < what.Count(); i++)
            {
                value = value * 29 + alphabet.IndexOf(what[i]);
            }

            return value;
        }
    }
}