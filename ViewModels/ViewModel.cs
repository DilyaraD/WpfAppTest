using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppTest.Models;
using WpfAppTest.Services;

namespace WpfAppTest.ViewModels
{
    public partial class ViewModel : ObservableObject
    {
        private readonly EqService _equipmentService;
        [ObservableProperty]
        private List<Equipment> _equipmentList;

        [ObservableProperty]
        private Equipment _selectedEquipment;

        [ObservableProperty]
        private bool _isEditMode;


        public ViewModel()
        {
            _equipmentService = new EqService();
            LoadEquipment();
        }

        [ObservableProperty]
        private List<string> _equipmentTypes = new List<string>
    {
        "Монитор",
        "Принтер",
        "Сканер"
    };

        [ObservableProperty]
        private List<string> _equipmentStatuses = new List<string>
    {
        "ВПользовании",
        "Наремонте",
        "НаСкладе"
    };

        [ObservableProperty]
        private Visibility _formVisibility = Visibility.Collapsed;

        /// <summary>
        /// вывод списка оборудований в DataGrid
        /// </summary>
        private void LoadEquipment()
        {
            EquipmentList = _equipmentService.GetAllEquipment();
        }

        /// <summary>
        /// Удаление выбранного оборудования
        /// </summary>
        [RelayCommand]
        private void DeleteEq()
        {
            if (SelectedEquipment != null)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить оборудование '{SelectedEquipment.Name}'?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _equipmentService.DeleteEquipment(SelectedEquipment.Id);
                    LoadEquipment();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите оборудование для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        /// Добавление нового оборудования
        /// </summary>
        [RelayCommand]
        private void AddEq()
        {
            
            SelectedEquipment = new Equipment();            
            IsEditMode = true;
        }

        /// <summary>
        /// Редактирование выбранного оборудования
        /// </summary>
        [RelayCommand]
        private void EditEq()
        {
            if (SelectedEquipment != null)
            {
                IsEditMode = true;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите оборудование для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Сохранение нового оборудования или изменений
        /// </summary>
        [RelayCommand]
        private void SaveEq()
        {
            if (string.IsNullOrWhiteSpace(SelectedEquipment?.Name))
            {
                MessageBox.Show("Название оборудования не может быть пустым",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(SelectedEquipment);
            bool isValid = Validator.TryValidateObject(SelectedEquipment, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedEquipment.Id == 0)
            {
                _equipmentService.AddEquipment(SelectedEquipment);
            }
            else
            {
                _equipmentService.UpdateEquipment(SelectedEquipment);
            }

            LoadEquipment();
        }

        /// <summary>
        /// Отмена редактирования - закрытие формы
        /// </summary>
        [RelayCommand]
        private void CancelEdit()
        {
            IsEditMode = false;
            LoadEquipment();
        }
    }
}
