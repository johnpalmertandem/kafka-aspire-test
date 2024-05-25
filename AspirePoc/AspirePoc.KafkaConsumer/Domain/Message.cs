namespace AspirePoc.KafkaConsumer.Domain;

public class Message
{
    public required int Id { get; set; }
    
    public required string MessageBody { get; set; }
    
    public required DateTimeOffset DateCreated { get; set; }
}