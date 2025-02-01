using System;

namespace PosTech.Hackathon.Users.Infra.Interfaces;

public interface IProducer
{
    void PublishMessageOnQueue<T>(T messageBody, string queueName);
}
