using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra
{
    public class MongoContext : IDbContext
    {
        private readonly List<Func<Task>> _commands;
        public IClientSessionHandle Session { get; set; }

        public MongoContext()
        {
            _commands = new List<Func<Task>>();
        }

        public string ConnectionString { get; set; }
        public string DataBase { get; set; }

        const string REGISTER_IGNORE_CONVENTION = "IgnoreConvention";
        const string REGISTER_ENUM_CONVENTION = "EnumConvention";

        public IMongoDatabase Context
        {
            get
            {
                MongoUrl url = new(this.ConnectionString);


                MongoClient client = new MongoClient(url);

                ConventionRegistry.Register(REGISTER_IGNORE_CONVENTION, new ConventionPack
                {
                    new IgnoreIfDefaultConvention(true),
                    new IgnoreExtraElementsConvention(true)
                }, t => true);

                ConventionRegistry.Register(REGISTER_ENUM_CONVENTION, new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, t => true);

                return client.GetDatabase(this.DataBase, new MongoDatabaseSettings { GuidRepresentation = GuidRepresentation.Standard });
            }
        }

        public async Task<int> SaveChanges()
        {
            var client = new MongoClient(this.ConnectionString);

            using (var session = await client.StartSessionAsync())
            {
                session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
