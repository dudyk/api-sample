using CarryLoad.Application.Core.Settings;
using CarryLoad.Application.OrderRequest.Services.Interfaces;
using CarryLoad.Application.OrderRequest.Services.Models;
using CarryLoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarryLoad.Application.OrderRequest.Services
{
    public class OrderWinnerService : IOrderWinnerService
    {
        private readonly OrderRequestSettings _settings;

        public OrderWinnerService(OrderRequestSettings settings)
        {
            _settings = settings;
        }

        public OrderWinnersModel GetWinnerIds(CarryLoad.Models.Entities.OrderRequest orderRequest, double targetWeight)
        {
            var vehicles = orderRequest.Vehicles
                .Where(r => r.StatusType == Enums.OrderRequestVehicleStatusTypes.UserAccepted)
                .Select(r => new VehicleWeightModel
                {
                    Id = r.Id,
                    Price = r.Price,
                    Weight = r.Vehicle.MaxWeight
                }).ToList();

            return orderRequest.VehicleServiceType switch
            {
                Enums.VehicleServiceTypes.TrawlingServices => SelectTrawlingWinnerIds(vehicles, targetWeight),
                Enums.VehicleServiceTypes.BulkCargo => SelectBulkCargoWinnerIds(vehicles, targetWeight),
                _ => throw new ArgumentOutOfRangeException($"Not implemented for type {orderRequest.VehicleServiceType}")
            };
        }

        private static OrderWinnersModel SelectTrawlingWinnerIds(IEnumerable<VehicleWeightModel> vehicles, double targetWeight)
        {
            if (!vehicles.Any())
                return new OrderWinnersModel();

            var vehicleIds = vehicles
                .Where(r => r.Weight >= targetWeight)
                .OrderBy(r => r.Price)
                .Select(r => r.Id)
                .Take(1);

            return new OrderWinnersModel
            {
                Ids = vehicleIds,
                IsWinnersFound = true
            };
        }

        public OrderWinnersModel SelectBulkCargoWinnerIds(IReadOnlyList<VehicleWeightModel> vehicles, double targetWeight)
        {
            if (!vehicles.Any()
                && targetWeight > vehicles.Sum(x => x.Weight))
                return new OrderWinnersModel();

            var combinations = CombineForWeight(vehicles, targetWeight, new List<VehicleWeightModel>());

            if (!combinations.Any())
                return new OrderWinnersModel();

            var vehicleIds = combinations
                .OrderBy(r => r.Sum(p => p.Price))
                .ThenBy(r => r.Sum(p => p.Weight))
                .ThenBy(r => r.Count())
                .ThenBy(r => r.Max(p => p.Weight))
                .First()
                .Select(r => r.Id);

            return new OrderWinnersModel
            {
                Ids = vehicleIds,
                IsWinnersFound = true
            };
        }

        private IEnumerable<IEnumerable<VehicleWeightModel>> CombineForWeight(
            IReadOnlyList<VehicleWeightModel> vehicles,
            double targetWeight,
            IReadOnlyCollection<VehicleWeightModel> items)
        {
            for (var i = 0; i < vehicles.Count; i++)
            {
                var combination = new List<VehicleWeightModel> { vehicles[i] };
                var weightLeft = targetWeight - combination.Sum(r => r.Weight);
                combination.AddRange(items);

                if (weightLeft < -_settings.WeightDelta)
                    continue;
                if (weightLeft <= 0)
                    yield return combination;

                var possible = vehicles
                    .Take(i)
                    .Where(r => r.Weight <= targetWeight
                                && !combination
                                    .Any(v => v.Id == r.Id))
                    .ToList();

                if (possible.Count > 0)
                    foreach (var item in CombineForWeight(possible, weightLeft, combination))
                        yield return item;
            }
        }

        // alt algorithm
        /*public IEnumerable<VehicleWeightModel> SelectWinnerIds(IEnumerable<VehicleWeightModel> vehicles, double targetWeight)
        {
            var items = vehicles
                .OrderBy(r => r.PriceWeight)
                .ThenByDescending(r => r.Weight)
                .ToList();

            var combination = new List<VehicleWeightModel>();

            foreach (var item in items)
            {
                var prevWeight = combination.Sum(r => r.Weight);
                var nextWeight = item.Weight + prevWeight;
                var weightLeft = targetWeight - nextWeight;

                if (weightLeft == 0)
                {
                    combination.Add(item);
                    return combination;
                }

                if (weightLeft > 0)
                {
                    combination.Add(item);
                    continue;
                }

                if (weightLeft < 0
                    && combination.Any())
                {
                    var last = items
                        .Where(r => !combination
                                        .Any(p => p.Id == r.Id)
                                    && r.Weight >= targetWeight - prevWeight)
                        .OrderBy(r => r.Price)
                        .First();

                    combination.Add(last);
                    return combination;
                }
            }

            return combination;
        }*/
    }
}
