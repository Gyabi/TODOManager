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
using TODOManager.Presentation.ViewModels.Contents;

namespace TODOManager.Presentation.Views
{
    /// <summary>
    /// TodoItemCard.xaml の相互作用ロジック
    /// </summary>
    public partial class TodoItemChildCard : UserControl
    {
        public TodoItemChildCard()
        {
            InitializeComponent();
        }

        //依存関係プロパティ
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(
                nameof(Item), //プロパティ名
                typeof(TodoItemChildVM), //バインドするデータの型
                typeof(TodoItemChildCard), //自分自身の型
                new PropertyMetadata(null) //初期値
        );

        public static readonly DependencyProperty DoneCommandProperty =
            DependencyProperty.Register(
                nameof(DoneCommand), //プロパティ名
                typeof(ICommand), //バインドするデータの型
                typeof(TodoItemChildCard), //自分自身の型
                new PropertyMetadata(null) //初期値
        );

        public TodoItemChildVM Item
        {
            get => (TodoItemChildVM)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }
        public ICommand DoneCommand
        {
            get => (ICommand)GetValue(DoneCommandProperty);
            set => SetValue(DoneCommandProperty, value);
        }
    }
}
