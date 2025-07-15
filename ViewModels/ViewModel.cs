using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppTest.Models;
using WpfAppTest.Services;

namespace WpfAppTest.ViewModels
{
    public partial class ViewModel : ObservableObject
    {
        private readonly EqService _equipmentService;
        [ObservableProperty]
        private List<Equipment> _equipmentList;

        public ViewModel()
        {
            _equipmentService = new EqService();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            EquipmentList = _equipmentService.GetAllEquipment();
        }
    }
}
