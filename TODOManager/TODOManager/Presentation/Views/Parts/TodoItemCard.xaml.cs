using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Presentation.Views
{
    /// <summary>
    /// TodoItemCard.xaml の相互作用ロジック
    /// </summary>
    public partial class TodoItemCard : UserControl
    {
        public TodoItemCard()
        {
            InitializeComponent();
        }

        //依存関係プロパティ
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                nameof(Item), //プロパティ名
                typeof(TodoItem), //バインドするデータの型
                typeof(TodoItemCard), //自分自身の型
                new PropertyMetadata(null) //初期値
        );

        public TodoItem Item
        {
            get => (TodoItem)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }
    }
}
