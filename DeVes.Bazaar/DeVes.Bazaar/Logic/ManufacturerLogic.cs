using System;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Logic
{
    public class ManufacturerLogic : BaseLogic<ManufacturerModel>, IManufacturerLogic
    {
        private readonly IManufacturerRepository _manufacturerRepository;


        public ManufacturerLogic(IManufacturerRepository manufacturerRepository)
            : base(manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        }


        public void Create(ManufacturerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            
            value.Number = value.Number <= 0
                ? _manufacturerRepository.GetNextFreeNumber()
                : value.Number;

            if (_manufacturerRepository.GetItem(value.Number) != null) throw new ArgumentException($"Number '{value.Number}' already in use!");

            _manufacturerRepository.Insert(value);
        }

        public void Update(ManufacturerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            
            if (_manufacturerRepository.GetItem(value.Number) == null) throw new ArgumentException($"{value.Number} not in use!");

            _manufacturerRepository.Update(value);
        }


        public void BasicInitialization()
        {
            if (_manufacturerRepository.Count > 0) return;

            Create(new ManufacturerModel { Title = "Völkl" });
            Create(new ManufacturerModel { Title = "Atomic" });
            Create(new ManufacturerModel { Title = "Fischer" });
            Create(new ManufacturerModel { Title = "Rossignol" });
            Create(new ManufacturerModel { Title = "Elan" });
            Create(new ManufacturerModel { Title = "K2" });
            Create(new ManufacturerModel { Title = "Dynastar" });
            Create(new ManufacturerModel { Title = "Blizzard" });
            Create(new ManufacturerModel { Title = "Head" });
            Create(new ManufacturerModel { Title = "Salomon" });
            Create(new ManufacturerModel { Title = "Nordica" });
            Create(new ManufacturerModel { Title = "Stöckli" });
            Create(new ManufacturerModel { Title = "Movement" });
            Create(new ManufacturerModel { Title = "Kästle" });
            Create(new ManufacturerModel { Title = "Armada" });
            Create(new ManufacturerModel { Title = "Scott" });
            Create(new ManufacturerModel { Title = "Line" });
            Create(new ManufacturerModel { Title = "Kneissl" });
            Create(new ManufacturerModel { Title = "Indigo" });
            Create(new ManufacturerModel { Title = "White Cristal" });
            Create(new ManufacturerModel { Title = "Dynamic" });
            Create(new ManufacturerModel { Title = "Black Diamond" });
            Create(new ManufacturerModel { Title = "Amplid" });
            Create(new ManufacturerModel { Title = "Bogner" });
            Create(new ManufacturerModel { Title = "Vist" });
            Create(new ManufacturerModel { Title = "Moment" });
            Create(new ManufacturerModel { Title = "Zag" });
            Create(new ManufacturerModel { Title = "CoreUPT" });
            Create(new ManufacturerModel { Title = "Hagan" });
            Create(new ManufacturerModel { Title = "4FRNT" });
            Create(new ManufacturerModel { Title = "Bohême" });
            Create(new ManufacturerModel { Title = "Surface" });
            Create(new ManufacturerModel { Title = "Pale" });
            Create(new ManufacturerModel { Title = "Majesty" });
            Create(new ManufacturerModel { Title = "Volant" });
            Create(new ManufacturerModel { Title = "Faction Skis" });
            Create(new ManufacturerModel { Title = "Zai" });
            Create(new ManufacturerModel { Title = "Hart" });
            Create(new ManufacturerModel { Title = "Trab" });
            Create(new ManufacturerModel { Title = "Lacroix" });
            Create(new ManufacturerModel { Title = "RTC" });
            Create(new ManufacturerModel { Title = "Liberty Skis" });
            Create(new ManufacturerModel { Title = "Zweydingers" });
            Create(new ManufacturerModel { Title = "Black Crows" });
            Create(new ManufacturerModel { Title = "Icelantic" });
            Create(new ManufacturerModel { Title = "DPS" });
            Create(new ManufacturerModel { Title = "G3" });
            Create(new ManufacturerModel { Title = "Blossom" });
            Create(new ManufacturerModel { Title = "GAF-Skis" });
            Create(new ManufacturerModel { Title = "Klint" });
            Create(new ManufacturerModel { Title = "Storm-Skis" });
            Create(new ManufacturerModel { Title = "Mountain Wave" });
            Create(new ManufacturerModel { Title = "Fat Ypus" });
            Create(new ManufacturerModel { Title = "Dynafit" });
            Create(new ManufacturerModel { Title = "Lightning Boards" });
            Create(new ManufacturerModel { Title = "Duret Skis" });
            Create(new ManufacturerModel { Title = "Voile" });
            Create(new ManufacturerModel { Title = "Prior" });
            Create(new ManufacturerModel { Title = "Extrem" });
            Create(new ManufacturerModel { Title = "Navaski" });
            Create(new ManufacturerModel { Title = "Kessler" });
            Create(new ManufacturerModel { Title = "Roxy" });
            Create(new ManufacturerModel { Title = "Differences" });
            Create(new ManufacturerModel { Title = "Born To Ride" });
            Create(new ManufacturerModel { Title = "Scotty Bob" });
            Create(new ManufacturerModel { Title = "Apo" });
            Create(new ManufacturerModel { Title = "Hammer" });
            Create(new ManufacturerModel { Title = "ON3P Skis" });
            Create(new ManufacturerModel { Title = "Pure Skis" });
            Create(new ManufacturerModel { Title = "Palmer" });
            Create(new ManufacturerModel { Title = "Duel" });
            Create(new ManufacturerModel { Title = "Avant" });
            Create(new ManufacturerModel { Title = "Core" });
            Create(new ManufacturerModel { Title = "Lokomotiv" });
            Create(new ManufacturerModel { Title = "Snowrider" });
            Create(new ManufacturerModel { Title = "Nocopy" });
            Create(new ManufacturerModel { Title = "Fortitude" });
            Create(new ManufacturerModel { Title = "Sochi" });
            Create(new ManufacturerModel { Title = "Ninthward" });
            Create(new ManufacturerModel { Title = "Epic Planks" });
            Create(new ManufacturerModel { Title = "Dupraz" });
            Create(new ManufacturerModel { Title = "Goodha" });
            Create(new ManufacturerModel { Title = "Whitedot" });
            Create(new ManufacturerModel { Title = "Schuetz Sports" });
            Create(new ManufacturerModel { Title = "High Society" });
            Create(new ManufacturerModel { Title = "Lagriffe" });
            Create(new ManufacturerModel { Title = "Powderequipment" });
            Create(new ManufacturerModel { Title = "Aluflex" });
            Create(new ManufacturerModel { Title = "Asnes" });
            Create(new ManufacturerModel { Title = "DeCosse Customs" });
            Create(new ManufacturerModel { Title = "Black Thunder" });
            Create(new ManufacturerModel { Title = "Eloura Blacksmith" });
            Create(new ManufacturerModel { Title = "AlpControl" });
            Create(new ManufacturerModel { Title = "UniQriding" });
            Create(new ManufacturerModel { Title = "Rax" });
            Create(new ManufacturerModel { Title = "Schärhorn" });
            Create(new ManufacturerModel { Title = "Heidiskis" });
            Create(new ManufacturerModel { Title = "Erbacher" });
            Create(new ManufacturerModel { Title = "Pogoski" });
            Create(new ManufacturerModel { Title = "Grown" });
            Create(new ManufacturerModel { Title = "Swell Panik" });
            Create(new ManufacturerModel { Title = "Clone Ind" });
            Create(new ManufacturerModel { Title = "Styibwe" });
            Create(new ManufacturerModel { Title = "Sterling" });
            Create(new ManufacturerModel { Title = "Foil" });
            Create(new ManufacturerModel { Title = "RootAlpine" });
        }
    }
}