﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkingLotApi.Models;

namespace ParkingLotApi.Repositories
{
    public class ParkingLotRepository:IParkingLotRepository
    {
        private readonly  IMongoCollection<ParkingLot> _parkingLotCollection;

        public ParkingLotRepository(IOptions<ParkinglotDatabaseSettings> parkingLotDatabaseSetting)
        {
            var mongoClient = new MongoClient(parkingLotDatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(parkingLotDatabaseSetting.Value.DatabaseName);
            _parkingLotCollection =
                mongoDatabase.GetCollection<ParkingLot>(parkingLotDatabaseSetting.Value.CollectionName);
        }

        public async Task<ParkingLot> CreateParkingLot(ParkingLot parkinglot)
        {
            await _parkingLotCollection.InsertOneAsync(parkinglot);
            return await _parkingLotCollection.Find(parking => parking.Id == parkinglot.Id).FirstAsync();
        }
    }
}