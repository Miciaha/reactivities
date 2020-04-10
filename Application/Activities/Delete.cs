using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            //Command Props
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly ILogger<Delete> _logger;

            public Handler(DataContext context, ILogger<Delete> logger)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                    throw new Exception("Could not find activity");
                
                _context.Remove(activity);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    _logger.LogInformation($"Deleted {activity.Title} activity");
                    return Unit.Value;

                }
                else
                {
                    throw new Exception("Problem saving changes");
                }


            }
        }
    }
}