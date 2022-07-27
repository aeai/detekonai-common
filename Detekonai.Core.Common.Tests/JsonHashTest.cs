using Detekonai.Core.Common.Runtime;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detekonai.Core.Common.Tests
{
    public class JsonHashTest
    {
		class ObjectWithStringAndKey {
			public string key;
		}
		class ObjectWithStringAndKfy
		{
			public string kfy;
		}
		class ObjectWithStringAndKeyButDifferent
		{
			public string key;
		}

		class ObjectWithIntegerAndKey
		{
			public int key;
		}

		class ObjectWithShortAndKey
		{
			public short key;
		}

		class ObjectWithIntegerAndKfy
		{
			public int kfy;
		}
		class ObjectWithIntegerAndKeyButDifferent
		{
			public int key;
		}

		class ObjectWithFloatAndKey
		{
			public float key;
		}
		class ObjectWithFloatAndKfy
		{
			public float kfy;
		}
		class ObjectWithFloatAndKeyButDifferent
		{
			public float key;
		}

		class MixedObject
		{
			public string strVal;
			public float realVal;
			public int intVal;
			public long longVal;
			public short shortVal;
			public byte byteVal;
		}

		class ObjectWithArray 
		{
			public ObjectWithIntegerAndKey[] keys = new ObjectWithIntegerAndKey[10];
		}

		[Test]
		public void Hash_works_beetwen_sessions()
		{
			var hash = new JsonHash();
			var ows = new MixedObject();
			ows.strVal = "value";
			ows.realVal = 1234.234f;
			ows.intVal = 1234;
			ows.longVal = 12341234;
			ows.shortVal = 1234;
			ows.byteVal = 123;

			JToken ob = JObject.FromObject(ows);
			uint res1 = hash.Calculate(ob);
			uint res2 = hash.Calculate(ob);
			Assert.That(res1, Is.EqualTo(res2));
			Assert.That(res1, Is.EqualTo(2254156296));
		}

		[Test]
		public void String_hash_equals_with_same_object_content()
		{
			JsonHash hash = new JsonHash();
			var ows1 = new ObjectWithStringAndKey();
			ows1.key = "value";
			var ows2 = new ObjectWithStringAndKey();
			ows2.key = "value";
			var ows3 = new ObjectWithStringAndKeyButDifferent();
			ows3.key = "value";

			JToken ob = JObject.FromObject(ows1);
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint ows2Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows3);
			uint ows3Hash = hash.Calculate(ob);
			Assert.That(ows1Hash, Is.EqualTo(ows2Hash));
			Assert.That(ows1Hash, Is.EqualTo(ows3Hash));
		}

		[Test]
		public void String_hash_differs_with_different_object_content()
		{
			var hash = new JsonHash();
			var ows1 = new ObjectWithStringAndKey();
			ows1.key = "value";
			var ows2 = new ObjectWithStringAndKey();
			ows2.key = "vblue";
			var ows3 = new ObjectWithStringAndKfy();
			ows3.kfy = "value";
			var ows4 = new ObjectWithStringAndKeyButDifferent();
			ows4.key = "vblue";
			JToken ob = JObject.FromObject(ows1);
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint ows2Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows3);
			uint ows3Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows4);
			uint ows4Hash = hash.Calculate(ob);

			Assert.That(ows1Hash, Is.Not.EqualTo(ows2Hash));
			Assert.That(ows1Hash, Is.Not.EqualTo(ows3Hash));
			Assert.That(ows1Hash, Is.Not.EqualTo(ows4Hash));
		}

		[Test]
		public void Integer_hash_differs_with_different_object_content()
		{
			var hash = new JsonHash();
			var ows1 = new ObjectWithIntegerAndKey();
			ows1.key = 1234;		 
			var ows2 = new ObjectWithIntegerAndKey();
			ows2.key = 1334;		
			var ows3 = new ObjectWithIntegerAndKfy();
			ows3.kfy = 1234;		 
			var ows4 = new ObjectWithIntegerAndKeyButDifferent();
			ows4.key = 1334;
			JToken ob = JObject.FromObject(ows1);
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint ows2Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows3);
			uint ows3Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows4);
			uint ows4Hash = hash.Calculate(ob);

			Assert.That(ows1Hash, Is.Not.EqualTo(ows2Hash));
			Assert.That(ows1Hash, Is.Not.EqualTo(ows3Hash));
			Assert.That(ows1Hash, Is.Not.EqualTo(ows4Hash));
		}

		[Test]
		public void Integer_hash_equals_with_same_object_content()
		{
			JsonHash hash = new JsonHash();
			var ows1 = new ObjectWithIntegerAndKey();
			ows1.key = 1234;		 
			var ows2 = new ObjectWithIntegerAndKey();
			ows2.key = 1234;		 
			var ows3 = new ObjectWithIntegerAndKeyButDifferent();
			ows3.key = 1234;
			var ows4 = new ObjectWithShortAndKey();
			ows4.key = 1234;

			JToken ob = JObject.FromObject(ows1);
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint ows2Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows3);
			uint ows3Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows4);
			uint ows4Hash = hash.Calculate(ob);
			Assert.That(ows1Hash, Is.EqualTo(ows2Hash));
			Assert.That(ows1Hash, Is.EqualTo(ows3Hash));
			Assert.That(ows1Hash, Is.EqualTo(ows4Hash));
		}
		[Test]
		public void Float_hash_differs_with_different_object_content()
		{
			var hash = new JsonHash();
			var ows1 = new ObjectWithFloatAndKey();
			ows1.key = 1234.345f;		 
			var ows2 = new ObjectWithFloatAndKey();
			ows2.key = 1234.445f;		 
			var ows3 = new ObjectWithFloatAndKfy();
			ows3.kfy = 1234.345f;		 
			var ows4 = new ObjectWithFloatAndKeyButDifferent();
			ows4.key = 1334.345f;
			JToken ob = JObject.FromObject(ows1);
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint ows2Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows3);
			uint ows3Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows4);
			uint ows4Hash = hash.Calculate(ob);

			Assert.That(ows1Hash, Is.Not.EqualTo(ows2Hash));
			Assert.That(ows1Hash, Is.Not.EqualTo(ows3Hash));
			Assert.That(ows1Hash, Is.Not.EqualTo(ows4Hash));
		}

		[Test]
		public void Float_hash_equals_with_same_object_content()
		{
			JsonHash hash = new JsonHash();
			var ows1 = new ObjectWithFloatAndKey();
			ows1.key = 1234.345f;		 
			var ows2 = new ObjectWithFloatAndKey();
			ows2.key = 1234.345f;		 
			var ows3 = new ObjectWithFloatAndKeyButDifferent();
			ows3.key = 1234.345f;

			JToken ob = JObject.FromObject(ows1);
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint ows2Hash = hash.Calculate(ob);
			ob = JObject.FromObject(ows3);
			uint ows3Hash = hash.Calculate(ob);
			Assert.That(ows1Hash, Is.EqualTo(ows2Hash));
			Assert.That(ows1Hash, Is.EqualTo(ows3Hash));
		}

		[Test]
		public void Hash_works_with_arrays()
		{
			var hash = new JsonHash();
			var ows1 = new ObjectWithArray();
			var ows2 = new ObjectWithArray();
			for (int i = 0; i< ows1.keys.Length; i++)
            {
				ows1.keys[i] = new ObjectWithIntegerAndKey();
				ows2.keys[i] = new ObjectWithIntegerAndKey();
				ows1.keys[i].key = i * 25;
				ows2.keys[i].key = i * 25;
			}
			JToken ob = JObject.FromObject(ows1);
			uint res1 = hash.Calculate(ob);
			ob = JObject.FromObject(ows2);
			uint res2 = hash.Calculate(ob);
			Assert.That(res1, Is.EqualTo(res2));
		}

		[Test]
		public void Object_order_doesnt_matter()
		{
			var hash = new JsonHash();
			JToken ob = JObject.Parse("{\"thing\":12, \"fruit\":\"apple\"}");
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.Parse("{\"fruit\":\"apple\", \"thing\":12}");
			uint ows2Hash = hash.Calculate(ob);

			Assert.That(ows1Hash, Is.EqualTo(ows2Hash));
		}

		[Test]
		public void Object_format_doesnt_matter()
		{
			var hash = new JsonHash();
			JToken ob = JObject.Parse("{\"thing\":12, \"fruit\":\"apple\"}");
			uint ows1Hash = hash.Calculate(ob);
			ob = JObject.Parse("{\"thing\":12, \n				\"fruit\":\"apple\"                              }");
			uint ows2Hash = hash.Calculate(ob);

			Assert.That(ows1Hash, Is.EqualTo(ows2Hash));
		}
	}
}
