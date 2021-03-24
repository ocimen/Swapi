using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using Swapi.Service.Interfaces;

namespace GrpcServer.Services
{
    public class GrpcPlanetService : Planet.PlanetBase
    {
        private readonly IPlanetService _planetService;

        public GrpcPlanetService(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        public override Task<PlanetModel> GetPlanetById(GetPlanetByIdModel request, ServerCallContext context)
        {
            var planet = _planetService.GetById(request.Id).Result;
            if (planet != null)
            {
                var planetModel = MapToPlanetModel(planet);
                return Task.FromResult(planetModel);
            }

            return null;
        }
        public override async Task SearchPlanet(SearchPlanetRequest request, IServerStreamWriter<PlanetModel> responseStream, ServerCallContext context)
        {
            var planetList = _planetService.Search(request.Name, request.Page).Result;
            var searchResult = planetList.results.Select(s => MapToPlanetModel(s)).ToList();
            foreach (var planet in searchResult)
            {
                await responseStream.WriteAsync(planet);
            }
        }

        private PlanetModel MapToPlanetModel(Swapi.Service.Models.Planet planet)
        {
            var planetModel = new PlanetModel()
            {
                Name = planet.Name,
                Climate = planet.Climate,
                Diameter = planet.Diameter,
                Gravity = planet.Gravity,
                OrbitalPeriod = planet.OrbitalPeriod,
                Population = planet.Population,
                RotationPeriod = planet.RotationPeriod,
                SurfaceWater = planet.SurfaceWater,
                Terrain = planet.Terrain
            };

            //TODO: find a better solution for list items
            foreach (var film in planet.Films)
            {
                planetModel.Films.Add(new PlanetFilms { Title = film });
            }

            foreach (var resident in planet.Residents)
            {
                planetModel.Residents.Add(new Residents { Title = resident });
            }

            return planetModel;
        }
    }
}