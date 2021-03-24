using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer;

namespace Swapi.Grpc.Console.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var grpcChannel = GrpcChannel.ForAddress("http://localhost:5100");

            // PEOPLE

            // Get People By Id
            System.Console.WriteLine("Sending request for getting People who has id 2");
            var peopleData = new GetPeopleByIdModel { Id = 2 };
            var peopleClient = new People.PeopleClient(grpcChannel);
            var peopleResponse = await peopleClient.GetPeopleByIdAsync(peopleData);
            System.Console.WriteLine(peopleResponse);
            System.Console.WriteLine("Press any key to continue for Search People");
            System.Console.ReadLine();

            // Search People
            System.Console.WriteLine("Sending request for search People whose name contains C");
            using (var clientData = peopleClient.SearchPeople(new SearchPeopleRequest {Name = "C", Page = 1}))
            {
                while (await clientData.ResponseStream.MoveNext(new CancellationToken()))
                {
                    var people = clientData.ResponseStream.Current;
                    System.Console.WriteLine(people);
                }
            }
            System.Console.WriteLine("Press any key to continue for Get Planet By Id");
            System.Console.ReadLine();

            // PLANET

            // Get Planet By Id
            System.Console.WriteLine("Sending request for getting Planet who has id 2");
            var planetData = new GetPlanetByIdModel { Id = 2 };
            var planetClient = new Planet.PlanetClient(grpcChannel);
            var planetResponse = await planetClient.GetPlanetByIdAsync(planetData);
            System.Console.WriteLine(planetResponse);
            System.Console.WriteLine("Press any key to continue for Search Planet");
            System.Console.ReadLine();

            // Search Planet
            System.Console.WriteLine("Sending request for search Planet whose name contains C");
            using (var clientData = planetClient.SearchPlanet(new SearchPlanetRequest { Name = "C", Page = 1 }))
            {
                while (await clientData.ResponseStream.MoveNext(new CancellationToken()))
                {
                    var planet = clientData.ResponseStream.Current;
                    System.Console.WriteLine(planet);
                }
            }

            System.Console.ReadLine();
        }
    }
}
