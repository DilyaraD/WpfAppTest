using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest.Models 
{
    /// <summary>
    /// Создана Модель, где есть класс Оборудование
    /// Тип (принтер, сканер, монитор)
    /// Статус (В пользовании, на складе, на ремонте)
    /// </summary>
    public partial class Equipment : ObservableObject
    {
        [ObservableProperty]
        public int _id;

        [ObservableProperty]
        public string _name = string.Empty;

        [ObservableProperty]
        public EqType _type;

        [ObservableProperty]
        public EqStatus _status;
    }

    public enum EqType { Принтер, Сканер, Монитор } 
    public enum EqStatus { ВПользовании, НаСкладе, НаРемонте }
}
