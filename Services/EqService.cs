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

        public List<Equipment> GetAllEquipment() => _equipmentList;
    }
}
