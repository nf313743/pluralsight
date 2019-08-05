using System;
using CSharpFunctionalExtensions;
using Logic.Students;
using Newtonsoft.Json;

namespace Logic.Decoractors
{
    public sealed class AuditLoggingDecoractor<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;

        public AuditLoggingDecoractor(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public Result Handle(TCommand command)
        {
            string commandJson = JsonConvert.SerializeObject(command);
            Console.WriteLine($"Command of type {command.GetType().Name}: { commandJson}");
            return _handler.Handle(command);
        }
    }
}