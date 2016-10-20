using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decoder
{
    public class Result
    {
        public string DecodedText { get; set; }
        public ResultCode CodeResult { get; set; }

        public Result(string decodedText, ResultCode codeResult)
        {
            this.DecodedText = decodedText;
            this.CodeResult = codeResult;
        }
    }

    public enum ResultCode
    {
        SUCCESS, FAILURE
    }

    public abstract class IDecoder
    {
        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IDecoder GetInstance()
        {
            /* TO DO - create your object */
            // decoderInstance = new ...;
            // return decoderInstance;
            throw new NotImplementedException();
        }

        public abstract Result decode(long searchedValue, string alphabet);

        protected static IDecoder decoderInstance;
    }
}
