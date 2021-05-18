using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public class ManufacturerLogic : IManufacturerLogic
    {
        private readonly IManufacturerRepository _manufacturerRepository;


        public ManufacturerLogic(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository =
                manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        }

        public ManufacturerModel              GetItem(long number) => _manufacturerRepository.GetItem(number);
        public IEnumerable<ManufacturerModel> GetItems()           => _manufacturerRepository.GetItems();


        public async Task<bool> CreateAsync(ManufacturerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value.Title))
                throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");

            value.Number = value.Number <= 0
                               ? _manufacturerRepository.GetNextFreeNumber()
                               : value.Number;

            if (_manufacturerRepository.GetItem(value.Number) != null)
                throw new ArgumentException($"Number '{value.Number}' already in use!");

            return await _manufacturerRepository.InsertAsync(value);
        }

        public async Task<bool> UpdateAsync(ManufacturerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title))
                throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");

            if (_manufacturerRepository.GetItem(value.Number) == null)
                throw new ArgumentException($"Number '{value.Number}' not in use!");

            return await _manufacturerRepository.UpdateAsync(value.Number, value);
        }

        public async Task<bool> DeleteAsync(long number)
        {
            if (number <= 0) throw new ArgumentException($"'{nameof(number)}' is not defined!");

            return await _manufacturerRepository.DeleteAsync(number);
        }


        public async Task BasicInitializationAsync()
        {
            if (_manufacturerRepository.Count > 0) return;

            await CreateAsync(new ManufacturerModel {Title = "Völkl"});
            await CreateAsync(new ManufacturerModel {Title = "Atomic"});
            await CreateAsync(new ManufacturerModel {Title = "Fischer"});
            await CreateAsync(new ManufacturerModel {Title = "Rossignol"});
            await CreateAsync(new ManufacturerModel {Title = "Elan"});
            await CreateAsync(new ManufacturerModel {Title = "K2"});
            await CreateAsync(new ManufacturerModel {Title = "Dynastar"});
            await CreateAsync(new ManufacturerModel {Title = "Blizzard"});
            await CreateAsync(new ManufacturerModel {Title = "Head"});
            await CreateAsync(new ManufacturerModel {Title = "Salomon"});
            await CreateAsync(new ManufacturerModel {Title = "Nordica"});
            await CreateAsync(new ManufacturerModel {Title = "Stöckli"});
            await CreateAsync(new ManufacturerModel {Title = "Movement"});
            await CreateAsync(new ManufacturerModel {Title = "Kästle"});
            await CreateAsync(new ManufacturerModel {Title = "Armada"});
            await CreateAsync(new ManufacturerModel {Title = "Scott"});
            await CreateAsync(new ManufacturerModel {Title = "Line"});
            await CreateAsync(new ManufacturerModel {Title = "Kneissl"});
            await CreateAsync(new ManufacturerModel {Title = "Indigo"});
            await CreateAsync(new ManufacturerModel {Title = "White Cristal"});
            await CreateAsync(new ManufacturerModel {Title = "Dynamic"});
            await CreateAsync(new ManufacturerModel {Title = "Black Diamond"});
            await CreateAsync(new ManufacturerModel {Title = "Amplid"});
            await CreateAsync(new ManufacturerModel {Title = "Bogner"});
            await CreateAsync(new ManufacturerModel {Title = "Vist"});
            await CreateAsync(new ManufacturerModel {Title = "Moment"});
            await CreateAsync(new ManufacturerModel {Title = "Zag"});
            await CreateAsync(new ManufacturerModel {Title = "CoreUPT"});
            await CreateAsync(new ManufacturerModel {Title = "Hagan"});
            await CreateAsync(new ManufacturerModel {Title = "4FRNT"});
            await CreateAsync(new ManufacturerModel {Title = "Bohême"});
            await CreateAsync(new ManufacturerModel {Title = "Surface"});
            await CreateAsync(new ManufacturerModel {Title = "Pale"});
            await CreateAsync(new ManufacturerModel {Title = "Majesty"});
            await CreateAsync(new ManufacturerModel {Title = "Volant"});
            await CreateAsync(new ManufacturerModel {Title = "Faction Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Zai"});
            await CreateAsync(new ManufacturerModel {Title = "Hart"});
            await CreateAsync(new ManufacturerModel {Title = "Trab"});
            await CreateAsync(new ManufacturerModel {Title = "Lacroix"});
            await CreateAsync(new ManufacturerModel {Title = "RTC"});
            await CreateAsync(new ManufacturerModel {Title = "Liberty Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Zweydingers"});
            await CreateAsync(new ManufacturerModel {Title = "Black Crows"});
            await CreateAsync(new ManufacturerModel {Title = "Icelantic"});
            await CreateAsync(new ManufacturerModel {Title = "DPS"});
            await CreateAsync(new ManufacturerModel {Title = "G3"});
            await CreateAsync(new ManufacturerModel {Title = "Blossom"});
            await CreateAsync(new ManufacturerModel {Title = "GAF-Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Klint"});
            await CreateAsync(new ManufacturerModel {Title = "Storm-Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Mountain Wave"});
            await CreateAsync(new ManufacturerModel {Title = "Fat Ypus"});
            await CreateAsync(new ManufacturerModel {Title = "Dynafit"});
            await CreateAsync(new ManufacturerModel {Title = "Lightning Boards"});
            await CreateAsync(new ManufacturerModel {Title = "Duret Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Voile"});
            await CreateAsync(new ManufacturerModel {Title = "Prior"});
            await CreateAsync(new ManufacturerModel {Title = "Extrem"});
            await CreateAsync(new ManufacturerModel {Title = "Navaski"});
            await CreateAsync(new ManufacturerModel {Title = "Kessler"});
            await CreateAsync(new ManufacturerModel {Title = "Roxy"});
            await CreateAsync(new ManufacturerModel {Title = "Differences"});
            await CreateAsync(new ManufacturerModel {Title = "Born To Ride"});
            await CreateAsync(new ManufacturerModel {Title = "Scotty Bob"});
            await CreateAsync(new ManufacturerModel {Title = "Apo"});
            await CreateAsync(new ManufacturerModel {Title = "Hammer"});
            await CreateAsync(new ManufacturerModel {Title = "ON3P Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Pure Skis"});
            await CreateAsync(new ManufacturerModel {Title = "Palmer"});
            await CreateAsync(new ManufacturerModel {Title = "Duel"});
            await CreateAsync(new ManufacturerModel {Title = "Avant"});
            await CreateAsync(new ManufacturerModel {Title = "Core"});
            await CreateAsync(new ManufacturerModel {Title = "Lokomotiv"});
            await CreateAsync(new ManufacturerModel {Title = "Snowrider"});
            await CreateAsync(new ManufacturerModel {Title = "Nocopy"});
            await CreateAsync(new ManufacturerModel {Title = "Fortitude"});
            await CreateAsync(new ManufacturerModel {Title = "Sochi"});
            await CreateAsync(new ManufacturerModel {Title = "Ninthward"});
            await CreateAsync(new ManufacturerModel {Title = "Epic Planks"});
            await CreateAsync(new ManufacturerModel {Title = "Dupraz"});
            await CreateAsync(new ManufacturerModel {Title = "Goodha"});
            await CreateAsync(new ManufacturerModel {Title = "Whitedot"});
            await CreateAsync(new ManufacturerModel {Title = "Schuetz Sports"});
            await CreateAsync(new ManufacturerModel {Title = "High Society"});
            await CreateAsync(new ManufacturerModel {Title = "Lagriffe"});
            await CreateAsync(new ManufacturerModel {Title = "Powderequipment"});
            await CreateAsync(new ManufacturerModel {Title = "Aluflex"});
            await CreateAsync(new ManufacturerModel {Title = "Asnes"});
            await CreateAsync(new ManufacturerModel {Title = "DeCosse Customs"});
            await CreateAsync(new ManufacturerModel {Title = "Black Thunder"});
            await CreateAsync(new ManufacturerModel {Title = "Eloura Blacksmith"});
            await CreateAsync(new ManufacturerModel {Title = "AlpControl"});
            await CreateAsync(new ManufacturerModel {Title = "UniQriding"});
            await CreateAsync(new ManufacturerModel {Title = "Rax"});
            await CreateAsync(new ManufacturerModel {Title = "Schärhorn"});
            await CreateAsync(new ManufacturerModel {Title = "Heidiskis"});
            await CreateAsync(new ManufacturerModel {Title = "Erbacher"});
            await CreateAsync(new ManufacturerModel {Title = "Pogoski"});
            await CreateAsync(new ManufacturerModel {Title = "Grown"});
            await CreateAsync(new ManufacturerModel {Title = "Swell Panik"});
            await CreateAsync(new ManufacturerModel {Title = "Clone Ind"});
            await CreateAsync(new ManufacturerModel {Title = "Styibwe"});
            await CreateAsync(new ManufacturerModel {Title = "Sterling"});
            await CreateAsync(new ManufacturerModel {Title = "Foil"});
            await CreateAsync(new ManufacturerModel {Title = "RootAlpine"});
        }
    }
}