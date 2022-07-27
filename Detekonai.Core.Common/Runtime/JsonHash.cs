using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detekonai.Core.Common.Runtime
{
	public class JsonHash
	{

		private readonly Dictionary<JTokenType, Func<JToken, uint>> hashFunctions;

		public JsonHash()
		{
			hashFunctions = new Dictionary<JTokenType, Func<JToken, uint>>
			 {
				 { JTokenType.Object, ContentsHashCode },
				 { JTokenType.Array, ContentsHashCode },
				 { JTokenType.Constructor, GetConstructorHashCode },
				 { JTokenType.Property, GetPropertyHashCode },
				 { JTokenType.String, GetPrimitiveStringHash },
				 { JTokenType.Integer, GetIntegerHash },
				 { JTokenType.Float, GetFloatHash },
				 { JTokenType.Boolean, GetBoolHash },
				 { JTokenType.Null, x => 0 },
				 { JTokenType.Comment, x => 0 },
				 { JTokenType.None, x => 0 },
				 { JTokenType.Undefined, x => 0 },
				 { JTokenType.Bytes, x => (uint)x.GetHashCode() },  //don't support this
				 { JTokenType.Date, GetDateHash },
				 { JTokenType.TimeSpan, GetTimeSpanHash },
				 { JTokenType.Raw, x => (uint)x.GetHashCode() },  //don't support this
				 { JTokenType.Uri, GetUriHash },
				 { JTokenType.Guid, GetGuidHash },
			 };

		}

		public uint Calculate(JToken token)
		{
			return hashFunctions[token.Type](token);
		}

		private uint ContentsHashCode(JToken token)
		{
			uint hashCode = 0;
			foreach (JToken item in token.Children())
			{
				hashCode ^= hashFunctions[item.Type](item);
			}
			return hashCode;
		}

		private uint GetConstructorHashCode(JToken token)
		{
			uint hash = HashString(((JConstructor)token).Name);

			return hash ^ ContentsHashCode(token);
		}
		private uint GetPropertyHashCode(JToken token)
		{
			uint hash = HashString(((JProperty)token).Name);

			return hash ^ ContentsHashCode(token);
		}

		private uint GetPrimitiveStringHash(JToken token)
		{
			return HashString(token.Value<string>());
		}
		private uint GetIntegerHash(JToken token)
		{
			long val = token.Value<long>();
			byte[] dta = BitConverter.GetBytes(val);
			return MurmurHash3.Hash(dta, (uint)dta.Length, 19850922) ;
		}

		private uint GetFloatHash(JToken token)
		{
			float val = token.Value<float>();
			byte[] dta = BitConverter.GetBytes(val);
			return MurmurHash3.Hash(dta, (uint)dta.Length, 19850922);
		}

		private uint GetUriHash(JToken token)
		{
			Uri val = token.Value<Uri>();
			return HashString(val.AbsoluteUri);
		}

		private uint GetGuidHash(JToken token)
		{
			Guid val = token.Value<Guid>();
			byte[] dta = val.ToByteArray();
			return MurmurHash3.Hash(dta, (uint)dta.Length, 19850922);
		}

		private uint GetBoolHash(JToken token)
		{
			bool val = token.Value<bool>();
			byte[] dta = BitConverter.GetBytes(val);
			return MurmurHash3.Hash(dta, (uint)dta.Length, 19850922);
		}

		private uint GetDateHash(JToken token)
		{
			DateTime val = token.Value<DateTime>();
			TimeSpan ts = val - new DateTime(1970, 1, 1);
			byte[] dta = BitConverter.GetBytes(ts.TotalMilliseconds);
			return MurmurHash3.Hash(dta, (uint)dta.Length, 19850922);
		}
		private uint GetTimeSpanHash(JToken token)
		{
			TimeSpan val = token.Value<TimeSpan>();
			byte[] dta = BitConverter.GetBytes(val.TotalMilliseconds);
			return MurmurHash3.Hash(dta, (uint)dta.Length, 19850922);
		}

		private byte[] stringHashBuffer = new byte[512];
		
		public uint HashString(string val)
		{
			if(string.IsNullOrEmpty(val))
            {
				return 0;
            }

			if (System.Text.Encoding.UTF8.GetByteCount(val) < stringHashBuffer.Length)
			{
				uint len = (uint)System.Text.Encoding.UTF8.GetBytes(val, 0, val.Length, stringHashBuffer, 0);
				return MurmurHash3.Hash(stringHashBuffer, len, 19850922);
			}
			else 
			{
				return (uint)val.GetHashCode();
			}
		}

		
	}
}
