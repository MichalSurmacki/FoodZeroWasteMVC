using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Common.Dtos;
using Domain.Entities;
using MediatR;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Recipies.Commands
{
    public class CreateRecipieCommand : IRequest<RecipieDto>
    {
        public RecipieCreateDto Recipie { get; set; }
        public CreateRecipieCommand(RecipieCreateDto recipie)
        {
            Recipie = recipie;
        }
    }
    public class CreateRecipieCommandHandler : IRequestHandler<CreateRecipieCommand, RecipieDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateRecipieCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<RecipieDto> Handle(CreateRecipieCommand request, CancellationToken cancellationToken)
        {
            //zamieniam createRecipieDto na Recipie - bez ID
            var recipie = new Recipie();

            recipie.Title = "TEST";
            recipie.Servings = 0;
            recipie.AllCarbs = 0;
            recipie.AllFat = 0;
            recipie.AllKcal = 0;
            recipie.AllProtein = 0;

            var cmp = new RecipieComponent
            {
                Recipie = recipie,
                SubTitle = "test"
            };
            var ing = new List<Ingredient>();
            ing.Add(new Ingredient { Name = "test",
            Amount = 0,
            Unit = "test",
            Kcal = 0,
            RecipieComponent = cmp});
            cmp.Ingredients = ing;
            var components = new List<RecipieComponent>();
            components.Add(cmp);

            recipie.Components = components;

            var instructionSteps = new List<InstructionStep>();
            instructionSteps.Add(new InstructionStep { Step = "test",
            StepOrder = 0,
            Recipie = recipie});

            recipie.InstructionSteps = instructionSteps;

            var tags = new List<Tag>();
            tags.Add(new Tag { Value = "test", Recipie = recipie });

            recipie.Tags = tags;

            var img = new List<Image>();
            img.Add(new Image { Url = "test", Recipie = recipie });

            recipie.Images = img;

            //dodaje do bazy danych z której biorę ID z typu Recipie
            //_context.Set<Recipie>().Add(recipie);
            _context.Recipies.Add(recipie);
            _context.SaveChanges();

            Guid id = recipie.Id;

            //mapuje Recipie z bazy na RecipieDto
            var recipieDto = _mapper.Map<RecipieDto>(recipie);
            
            //zwracam utworzone recipie jako recipieDto
            return Task.FromResult(recipieDto);
        }
    }
}