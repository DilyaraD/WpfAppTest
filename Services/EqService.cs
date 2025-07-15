using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppTest.Models;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WpfAppTest.Services
{
    /// <summary>
    /// Если файл с данными еще не был создан, то он создается и заполняется данными. Если уже существует, то сразу используется.
    /// </summary>
    public class EqService
    {
        private readonly string _dataFilePath = "eqData.json";
        private List<Equipment> _equipmentList = new List<Equipment>();

        public EqService()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            if (File.Exists(_dataFilePath))
            {
                var json = File.ReadAllText(_dataFilePath);
                _equipmentList = JsonConvert.DeserializeObject<List<Equipment>>(json) ?? new List<Equipment>();
            }
            else
            {
                _equipmentList = new List<Equipment>
                {
                    new Equipment { Id = 1, Name = "HP LaserJet", Type = EqType.Принтер, Status = EqStatus.ВПользовании },
                    new Equipment { Id = 2, Name = "Epson Perfection", Type = EqType.Сканер, Status = EqStatus.НаСкладе },
                    new Equipment { Id = 3, Name = "Dell UltraSharp", Type = EqType.Монитор, Status = EqStatus.НаРемонте },
                    new Equipment { Id = 4, Name = "Dell UltraSharp", Type = EqType.Монитор, Status = EqStatus.ВПользовании },
                    new Equipment { Id = 5, Name = "Epson Perfection", Type = EqType.Сканер, Status = EqStatus.НаРемонте }
                };

                SaveData();
            }
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(_equipmentList, Formatting.Indented);
            File.WriteAllText(_dataFilePath, json);
        }

        /// <summary>
        /// Получение списка оборудований
        /// </summary>
        public List<Equipment> GetAllEquipment() => _equipmentList;

        /// <summary>
        /// Получение конкретного оборудования по id
        /// </summary>
        public Equipment GetEquipmentById(int id) => _equipmentList.FirstOrDefault(e => e.Id == id);

         /// <summary>
         /// Добавление нового оборудования, айди для нового = максимальное айди+1
         /// </summary>
        public void AddEquipment(Equipment equipment)
        {
            equipment.Id = _equipmentList.Any() ? _equipmentList.Max(e => e.Id) + 1 : 1;
            _equipmentList.Add(equipment);
            SaveData();
            InitializeData();
        }

        /// <summary>
        /// Обновить существующее оборудование, находя его по айди
        /// </summary>
        public void UpdateEquipment(Equipment equipment)
        {
            var existing = _equipmentList.FirstOrDefault(e => e.Id == equipment.Id);
            if (existing != null)
            {
                existing.Name = equipment.Name;
                existing.Type = equipment.Type;
                existing.Status = equipment.Status;
                SaveData();
                InitializeData();
            }
        }

        /// <summary>
        /// Удалить оборудование, находя его по айди
        /// </summary>
        public void DeleteEquipment(int id)
        {
            var equipment = _equipmentList.FirstOrDefault(e => e.Id == id);
            if (equipment != null)
            {
                _equipmentList.Remove(equipment);
                SaveData();
                InitializeData();
            }
        }
    }
}
