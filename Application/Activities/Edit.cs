using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }

            public DateTime? Date { get; set; }

            public string City { get; set; }

            public string Venue { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly ILogger<Edit> _logger;

            public Handler(DataContext context, ILogger<Edit> logger)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if(activity == null)
                    throw new Exception("Could not find activity");
                
                activity.Title = request.Title ?? activity.Title;
                activity.Description = request.Description ?? activity.Description;
                activity.Category = request.Category ?? activity.Category;
                activity.City = request.City ?? activity.City;
                activity.Date = request.Date ?? activity.Date;
                activity.Venue = request.Venue ?? activity.Venue;
                                
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    _logger.LogInformation($"Edited {activity.Title} activity");
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