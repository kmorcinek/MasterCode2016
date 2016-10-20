using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
	public enum ParseStatus{
		PARSE_STATUS_OK,
		PARSE_STATUS_MORE_DATA,
		PARSE_STATUS_ERROR
	};
	
	public class vCardStruct{
		
		public String Name {get; set;}
		public String Address {get; set;}
		public String TelNumber {get; set;}
		public String Email {get; set;}
		public String PhotoData {get; set;}

		public vCardStruct(vCardStruct reference) {
			Name = reference.Name;
			Address = reference.Address;
			TelNumber = reference.TelNumber;
			Email = reference.Email;
			PhotoData = reference.PhotoData;
		}

        public vCardStruct()
        {
            Name = "";
            Address = "";
            TelNumber = "";
            Email = "";
            PhotoData = "";
        }
    }
	
	public abstract class IParserListener {
		public abstract void newVCard(vCardStruct vCard);
	}
	
    public abstract class IParser
    {
		protected static IParser m_parserInstance;

        /// <summary>
        /// Put implementation creating your derived class instance in derived class source code file.
        /// Use the 'new' keyword to overwrite default behaviour.
        /// </summary>
        /// <returns></returns>
        public static IParser GetInstance()
        {
            /* TO DO - create your object */
            // m_parserInstance = new ...;
            // return m_parserInstance;
            throw new NotImplementedException();
        }

		public abstract ParseStatus parse(String buffer);
		public abstract void setParserListener(IParserListener listener);

	}
}
