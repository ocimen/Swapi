using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Swapi.Service.Interfaces;

namespace GrpcServer.Services
{
    public class GrpcPeopleService : People.PeopleBase
    {
        private readonly IPeopleService _peopleService;

        public GrpcPeopleService(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public override Task<PeopleModel> GetPeopleById(GetPeopleByIdModel request, ServerCallContext context)
        {
            var people = _peopleService.GetById(request.Id).Result;
            if (people != null)
            {
                var peopleModel = MapToPeopleModel(people);
                return Task.FromResult(peopleModel);
            }

            return null;
        }
        public override async Task SearchPeople(SearchPeopleRequest request, IServerStreamWriter<PeopleModel> responseStream, ServerCallContext context)
        {
            var peopleList = _peopleService.Search(request.Name, request.Page).Result;
            var searchResult = peopleList.results.Select(s => MapToPeopleModel(s)).ToList();
            foreach (var people in searchResult)
            {
                await responseStream.WriteAsync(people);
            }
        }

        private PeopleModel MapToPeopleModel(Swapi.Service.Models.People people)
        {
            var peopleModel = new PeopleModel
            {
                Name = people.Name,
                Height = people.Height,
                Mass = people.Mass,
                HairColor = people.HairColor,
                SkinColor = people.SkinColor,
                EyeColor = people.EyeColor,
                BirthYear = people.BirthYear,
                Gender = people.Gender,
                Homeworld = people.Homeworld
            };

            //TODO: find a better solution for list items
            foreach (var film in people.Films)
            {
                peopleModel.Films.Add(new Films { Title = film });
            }

            foreach (var vehicle in people.Vehicles)
            {
                peopleModel.Vehicles.Add(new Vehicles {Title  = vehicle });
            }

            foreach (var specie in people.Species)
            {
                peopleModel.Species.Add(new Species { Title = specie });
            }

            foreach (var starship in people.Starships)
            {
                peopleModel.Starships.Add(new Starships { Title = starship });
            }

            return peopleModel;
        }
    }
}
