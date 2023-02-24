using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Grus.Domain.Entities.Budget;

public class BudgetItemsSerializer : SerializerBase<List<BudgetItem>>
{
    public override List<BudgetItem> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonReader = context.Reader;
        var items = new List<BudgetItem>();

        bsonReader.ReadStartArray();

        while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var document = BsonSerializer.Deserialize<BsonDocument>(bsonReader);
            var discriminator = document.GetValue("_t").AsString;

            switch (discriminator)
            {
                case "Income":
                    items.Add(BsonSerializer.Deserialize<Income>(document));
                    break;

                case "Bill":
                    items.Add(BsonSerializer.Deserialize<Bill>(document));
                    break;

                case "Savings":
                    items.Add(BsonSerializer.Deserialize<Savings>(document));
                    break;

                case "Subscription":
                    items.Add(BsonSerializer.Deserialize<Subscription>(document));
                    break;

                case "Loan":
                    items.Add(BsonSerializer.Deserialize<Loan>(document));
                    break;

                default:
                    throw new BsonSerializationException($"Unknown discriminator value '{discriminator}'.");
            }
        }

        bsonReader.ReadEndArray();

        return items;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, List<BudgetItem> value)
    {
        var bsonWriter = context.Writer;

        bsonWriter.WriteStartArray();

        foreach (var item in value)
        {
            BsonSerializer.Serialize(bsonWriter, item);
        }

        bsonWriter.WriteEndArray();
    }
}