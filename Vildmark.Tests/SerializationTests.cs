using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vildmark.Serialization;

namespace Vildmark.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void RoundTrip()
        {
            Serializer serializer = new Serializer();
            TestObject test = new TestObject
            {
                Value = 10,
                Name = "name"
            };

            byte[] data = serializer.Serialize(test);
            
            TestObject serialized = serializer.Deserialize<TestObject>(data);
            
            Assert.AreNotSame(test, serialized);
            Assert.AreEqual(test.Name, serialized.Name);
            Assert.AreEqual(test.Value, serialized.Value);
        }

        private class TestObject : ISerializable
        {
            public int Value { get; set; }

            public string Name { get; set; }

            public void Deserialize(IReader reader)
            {
                Value = reader.ReadValue<int>();
                Name = reader.ReadString();
            }

            public void Serialize(IWriter writer)
            {
                writer.WriteValue(Value);
                writer.WriteString(Name);
            }
        }
    }
}
