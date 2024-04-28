using CA.Application.Samples.Dtos;
using CA.Domain.Entities;
using CA.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace CA.Application.Samples.Commands
{
    public class AddSampleCommand : IRequest<SampleDto>
    {
        public string TestField1 { get; set; }
        public string TestField2 { get; set; }

        public class Validator : AbstractValidator<AddSampleCommand>
        {
            public Validator()
            {
                RuleFor(x => x.TestField1).NotEmpty().WithMessage("TestField1 must not be empty");
                RuleFor(x => x.TestField2).NotEmpty().WithMessage("TestField2 must not be empty");
            }
        }
    }

    public class Handler : IRequestHandler<AddSampleCommand, SampleDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<AddSampleCommand> _validator;
        private readonly IRepository<Sample, Guid> _sampleRepository;

        public Handler(
            IUnitOfWork uow,
            IValidator<AddSampleCommand> validator,
            IRepository<Sample, Guid> sampleRepository)
        {
            _uow = uow;
            _validator = validator;
            _sampleRepository = sampleRepository;
        }

        public async Task<SampleDto> Handle(AddSampleCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var sample = new Sample
            {
                TestField1 = request.TestField1,
                TestField2 = request.TestField2,
            };

            await _sampleRepository.AddAsync(sample, cancellationToken);
            await _uow.SaveChangesAsync();

            return new SampleDto
            {
                TestField1 = sample.TestField1,
                TestField2 = sample.TestField2,
            };
        }
    }
}
