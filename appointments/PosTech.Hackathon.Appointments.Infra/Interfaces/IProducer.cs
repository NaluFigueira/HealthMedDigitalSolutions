namespace PosTech.Hackathon.Appointments.Infra.Interfaces;
public interface IProducer
{
    void PublishMessageOnQueue<T>(T messageBody, string queueName);
}
