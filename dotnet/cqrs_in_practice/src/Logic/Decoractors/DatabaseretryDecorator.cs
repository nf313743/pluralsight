using System;
using CSharpFunctionalExtensions;
using Logic.Students;
using Logic.Utils;

namespace Logic.Decoractors
{
    public sealed class DatabaseRetryDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly Config _config;

        public DatabaseRetryDecorator(ICommandHandler<TCommand> handler, Config config)
        {
            _handler = handler;
            _config = config;
        }

        public Result Handle(TCommand command)
        {
            for (int i = 0; ; ++i)
            {
                try
                {
                    var result = _handler.Handle(command);
                    return result;
                }
                catch (Exception ex)
                {
                    if (i >= _config.NumberOfDatabaseRetries || !IsDatabaseException(ex))
                    {
                        throw;
                    }
                }
            }
        }

        private bool IsDatabaseException(Exception ex)
        {
            var message = ex.InnerException?.Message;
            if (message == null)
                return false;

            return message.Contains("The connection is broke and recovery is not possible")
                || message.Contains("error occurred while establishing a connection");
        }
    }
}